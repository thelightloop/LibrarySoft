namespace Library.Common.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public T? Data { get; init; }
        public int StatusCode { get; init; }

        private ApiResponse(bool success, string message, T? data = default, int statusCode = 200)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success", int statusCode = 200)
            => new(true, message, data, statusCode);

        public static ApiResponse<T> SuccessMessage(string message = "Success", int statusCode = 200)
            => new(true, message, default, statusCode);

        public static ApiResponse<T> Fail(string message, int statusCode = 400)
            => new(false, message, default, statusCode);
    }
}