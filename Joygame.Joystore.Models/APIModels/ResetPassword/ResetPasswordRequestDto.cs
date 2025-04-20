namespace Joygame.Joystore.API.Models.ResetPassword
{
    public class ResetPasswordRequestDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

}
