using Joygame.Joystore.API.Exceptions;
using Joygame.Joystore.API.Extensions;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var passw = PasswordHasher.HashPassword(request.Password);

                var result = _authService.Login(request.Username, request.Password);
                _logger.LogInformation($"{request.Username} Logged in", request.Username);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                _logger.LogWarning(ex, "Login failed for user {Username}", request.Username);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message, request.Username);
                return StatusCode(500, "An unexpected error occurred on the server.");
            }

        }
    }
}
