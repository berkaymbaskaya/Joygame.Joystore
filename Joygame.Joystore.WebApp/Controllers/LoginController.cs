using Joygame.Joystore.API.Models.ForgotPassword;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Models.ResetPassword;
using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;

namespace Joygame.Joystore.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new LoginViewModel();

            if (Request.Cookies.TryGetValue("remembered_username", out var rememberedUsername))
            {
                model.Username = rememberedUsername;
                model.RememberMe = true;
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var loginDto = new LoginRequestDto
                {
                    Username = model.Username,
                    Password = model.Password
                };

                var result = await _authService.Login(loginDto);

                if (model.RememberMe)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(3)
                    };
                    Response.Cookies.Append("remembered_username", result.Data.User.UserName, cookieOptions);

                }

                HttpContext.Session.SetString("token", JsonConvert.SerializeObject(result.Data.Token));
                HttpContext.Session.SetString("username", result.Data.User.UserName);
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(result.Data.User));

                return RedirectToAction("Index", "Product");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                return View("Index", model);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("jwt_token");
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("Login/ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        [HttpPost("Login/ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var dto = new ForgotPasswordRequestDto
                {
                    Email = model.Email
                };      
                await _authService.ForgotPassword(dto);
                TempData["Success"] = "Password reset instructions have been sent to your email.";
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                TempData["Error"] = "Email not found";
                return View("ForgotPassword",model);
            }
        }


        [HttpGet("Login/ResetPassword")]
        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordViewModel
            {
                Token = token
            };

            return View(model);
        }

        [HttpPost("Login/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var dto = new ResetPasswordRequestDto
                {
                    Token = model.Token,
                    NewPassword = model.NewPassword
                };

                await _authService.ResetPassword(dto);

                TempData["Success"] = "Password successfully reset.";
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                TempData["Error"] = "Token expired or invalid.";
                return RedirectToAction("Index");
            }
        }
    }
}
