using Joygame.Joystore.API.Core;
using Joygame.Joystore.API.Models.ForgotPassword;
using Joygame.Joystore.API.Models.Login;
using Joygame.Joystore.API.Models.ResetPassword;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joygame.Joystore.Services.Interfaces
{
    public interface IAuthService
    {
        [Post("/Auth/Login")]
        Task<API.Core.ApiResponse<LoginResponseDto>> Login([Body] LoginRequestDto request);
        [Post("/Auth/forgot-password")]
        Task ForgotPassword([Body] ForgotPasswordRequestDto request);

        [Post("/Auth/reset-password")]
        Task ResetPassword([Body] ResetPasswordRequestDto request);

    }
}
