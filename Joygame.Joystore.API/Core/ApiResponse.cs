namespace Joygame.Joystore.API.Core
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public Error? Error { get; set; }
    }
    public class Error
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
        public string? Details { get; set; }

    }
}
