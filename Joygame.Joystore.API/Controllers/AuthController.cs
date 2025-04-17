using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Entities;
using Joygame.Joystore.API.Exceptions;
using Joygame.Joystore.API.Extensions;
using Joygame.Joystore.API.Models.ForgotPassword;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Models.ResetPassword;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using static Joygame.Joystore.API.Exceptions.AppExceptions;

namespace Joygame.Joystore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var passw = PasswordHasher.HashPassword(request.Password);

            var result = _authService.Login(request.Username, request.Password);
            _logger.LogInformation($"{request.Username} Logged in", request.Username);
            var response = new ApiResponse<LoginResponseDto>
            {
                Data = result,
                Success = true
            };
            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            await _authService.ForgotPassword(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            await _authService.ResetPasswordAsync(request);
            var response = new ApiResponse<string>
            {
                Data = null,
                Success = true
            };
            return Ok(response);

        }
    }
}
