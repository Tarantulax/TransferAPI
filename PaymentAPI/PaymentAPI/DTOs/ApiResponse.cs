namespace PaymentAPI.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data) => new ApiResponse<T>(true, string.Empty, data);
        public static ApiResponse<T> ErrorResponse(string message) => new ApiResponse<T>(false, message, default);
    }

}
