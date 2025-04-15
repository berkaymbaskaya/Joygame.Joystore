namespace Joygame.Joystore.API.Models.Login
{
    public class LoginResponseDto
    {
        public TokenDto Token { get; set; }
        public UserDto User { get; set; }

    }
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
    public class UserDto
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
