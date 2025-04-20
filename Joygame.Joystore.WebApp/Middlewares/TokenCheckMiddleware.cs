using Joygame.Joystore.API.Models.Login;
using Newtonsoft.Json;

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

            if (string.IsNullOrEmpty(tokenJson))
            {
                tokenJson = context.Request.Cookies["jwt_token"];

                if (!string.IsNullOrEmpty(tokenJson))
                {
                    context.Session.SetString("token", tokenJson);
                }
            }

            if (string.IsNullOrEmpty(tokenJson) && !path.StartsWith("/login"))
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
                        context.Response.Cookies.Delete("jwt_token");
                        context.Response.Redirect("/Login");
                        return;
                    }
                }
                catch
                {
                    context.Session.Remove("token");
                    context.Response.Cookies.Delete("jwt_token");
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
