using Joygame.Joystore.API.Models.Login;
using System.Security.Claims;

namespace Joygame.Joystore.API.Security
{
    public interface ITokenProvider
    {
        TokenDto GenerateToken(IEnumerable<Claim> claims);

    }
}
