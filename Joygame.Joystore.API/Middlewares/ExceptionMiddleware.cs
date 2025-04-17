using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Exceptions;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); 
        }
        catch (BaseException ex)
        {
            _logger.LogWarning(ex, $"Business rule exception occurred. Path: {httpContext.Request.Path}");

            var response = new ApiResponse<string>
            {
                Data = null,
                Success = false,
                Error = new Error
                {
                    Message = ex.Message,
                    Code = "400"
                }
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unhandled exception occurred. Path: {httpContext.Request.Path}");

            var response = new ApiResponse<string>
            {
                Data = null,
                Success = false,
                Error = new Error
                {
                    Message = "An unexpected error occurred on the server.",
                    Code = "500"
                }
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
