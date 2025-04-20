using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.Services.Interfaces;
using Joygame.Joystore.WebApp.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
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
            return RedirectToAction("Index", "Login");
        }



    }
}
