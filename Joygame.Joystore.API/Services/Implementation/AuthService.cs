using Joygame.Joystore.API.Contexts;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Security;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Joygame.Joystore.API.Services.Implementation
{
    public class AuthService: IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenProvider _tokenProvider;

        public AuthService(ApplicationDbContext context, ITokenProvider tokenProvider)
        {
            _context = context;
            _tokenProvider = tokenProvider;
        }
        public LoginResponseDto Login(string username, string password)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
            
            if (user == null)
                return null;

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Email", user.Email ?? "")
            };

            var tokenDto = _tokenProvider.GenerateToken(claims);
            var userDto = new UserDto
            {
                UserName = user.Username,
                UserId = user.Id.ToString(),
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };

            return new LoginResponseDto
            {
                Token= tokenDto,
                User= userDto
            };
        }
    }
}
