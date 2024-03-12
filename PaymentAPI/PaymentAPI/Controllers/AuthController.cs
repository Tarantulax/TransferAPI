using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PaymentAPI.DTOs;
using PaymentAPI.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public AuthController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<TokenResponse> Login(LoginRequest request)
        {
            var query = new Service.LoginServices.Query.GetLoginRequestQuery{ LoginRequest = request };
            var response = await _mediator.Send(query);
            if (response == 0)
            {
                return null;
            }
            var tokenResult = GenerateToken(response);
            return tokenResult;
        }

        private TokenResponse GenerateToken(int userId)
        {
            double expiryMinutes;
            if (!double.TryParse(_configuration["Jwt:Expiry"], out expiryMinutes))
            {
                throw new InvalidOperationException("Jwt:Expiry ayarı geçerli bir dakika değeri olmalıdır.");
            }

            var expiryDate = DateTime.UtcNow.AddMinutes(expiryMinutes);
            var claims = GenerateUserClaims(userId);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: expiryDate, signingCredentials: signIn);

            TokenResponse loginResult = new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiry = expiryDate
            };

            return loginResult;
        }
        private Claim[] GenerateUserClaims(int userId)
        {

            return  new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", userId.ToString()),
               };
        }
    }
}