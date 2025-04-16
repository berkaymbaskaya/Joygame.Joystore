using Joygame.Joystore.API.Models.ForgotPassword;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Models.ResetPassword;

namespace Joygame.Joystore.API.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDto Login(string username, string password);
        public Task ForgotPassword(ForgotPasswordRequestDto request);
        public Task ResetPasswordAsync(ResetPasswordRequestDto request);

    }
}
