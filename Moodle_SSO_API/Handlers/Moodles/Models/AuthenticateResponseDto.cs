namespace Moodle_SSO_API.Handlers.Moodles.Models
{
    public class AuthenticateResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Privatetoken { get; set; } = string.Empty;
        public string Errorcode { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public bool Success { get; set; }
        public List<GetUserResponseDto>? UserData { get; set; }
    }
} 