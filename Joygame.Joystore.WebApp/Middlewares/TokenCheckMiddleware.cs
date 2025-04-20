namespace Joygame.Joystore.WebApp.Middlewares
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("token");
            var path = context.Request.Path.Value?.ToLower();

            if (string.IsNullOrEmpty(token) && !path.StartsWith("/login"))
            {
                context.Response.Redirect("/Login");
                return;
            }

            await _next(context);
        }
    }

    public static class TokenCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenCheck(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenCheckMiddleware>();
        }
    }
}
