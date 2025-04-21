using Joygame.Joystore.API.Models.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Joygame.Joystore.API.Security
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(60);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiresAt
            };
        }

    }
}
