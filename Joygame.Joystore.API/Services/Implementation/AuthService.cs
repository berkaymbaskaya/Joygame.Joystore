using Joygame.Joystore.API.Contexts;
using Joygame.Joystore.API.Entities;
using Joygame.Joystore.API.Exceptions;
using Joygame.Joystore.API.Extensions;
using Joygame.Joystore.API.Models.ForgotPassword;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Models.ResetPassword;
using Joygame.Joystore.API.Security;
using Joygame.Joystore.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Joygame.Joystore.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<AuthService> _logger;

        public AuthService(ApplicationDbContext context, ITokenProvider tokenProvider, ILogger<AuthService> logger)
        {
            _context = context;
            _tokenProvider = tokenProvider;
            _logger = logger;
        }
        public LoginResponseDto Login(string username, string password)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new AppExceptions.UserNotFoundException("Invalid username or password.");
            }

            var verify = PasswordHasher.Verify(password, user.PasswordHash);

            if (!verify)
            {
                throw new AppExceptions.InvalidCredentialException("Invalid username or password.");
            }

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Email", user.Email ?? "")
            };
            claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Name)));

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
                Token = tokenDto,
                User = userDto
            };
        }

        public async Task ForgotPassword(ForgotPasswordRequestDto request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

                if (user == null)
                {
                    throw new AppExceptions.UserNotFoundException("User not found.");
                }

                var token = Guid.NewGuid().ToString("N");

                var resetToken = new PasswordResetToken
                {
                    UserId = user.Id,
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    CreatedAt = DateTime.UtcNow,
                    IsUsed = false,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedUser = user.Id
                };

                _context.PasswordResetTokens.Add(resetToken);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Forgot Password Token Created for email:{request.Email} ");
            }
            catch
            {
                throw;
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            try
            {
                var resetToken = _context.PasswordResetTokens
                        .FirstOrDefault(t =>
                            t.Token == request.Token &&
                            t.IsUsed == false &&
                            t.IsDeleted == false &&
                            t.IsActive == true &&
                            t.ExpiresAt > DateTime.UtcNow);

                if (resetToken == null)
                {
                    _logger.LogWarning($"Password reset failed (Invalid or expired token)");
                    throw new AppExceptions.InvalidTokenException("Invalid or expired token.");
                }

                var user = _context.Users.FirstOrDefault(u => u.Id == resetToken.UserId);

                if (user == null)
                {
                    _logger.LogWarning($"Password reset failed (User not found)");
                    throw new AppExceptions.UserNotFoundException("User not found.");
                }

                user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);
                user.UpdatedAt = DateTime.UtcNow;

                resetToken.IsUsed = true;
                resetToken.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Password successfully reset for UserId: {user.Id}");
            }
            catch
            {
                throw;
            }
    
        }

    }
}
