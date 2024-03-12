using PaymentAPI.Data;

namespace PaymentAPI.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, UserContext userContext)
        {
            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                userContext.UserId = userId;
            }

            await _next(httpContext);
        }
    }

}
