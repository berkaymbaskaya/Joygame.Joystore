using Joygame.Joystore.API.Models.Login;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var tokenJson = context.Session.GetString("token");
            var path = context.Request.Path.Value?.ToLower();
            var publicPaths = new[]
            {
                "/login",
                "/login/index",
                "/login/forgotpassword"
            };
            if (string.IsNullOrEmpty(tokenJson) && !publicPaths.Any(p => path.StartsWith(p)))
            {
                context.Response.Redirect("/Login");
                return;
            }

            if (!string.IsNullOrEmpty(tokenJson) && !path.StartsWith("/login"))
            {
                try
                {
                    var tokenDto = JsonConvert.DeserializeObject<TokenDto>(tokenJson);

                    if (string.IsNullOrEmpty(tokenDto.AccessToken) || tokenDto.Expiration < DateTime.UtcNow)
                    {
                        context.Session.Remove("token");
                        context.Response.Redirect("/Login");
                        return;
                    }
                }
                catch
                {
                    context.Session.Remove("token");
                    context.Response.Redirect("/Login");
                    return;
                }
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
