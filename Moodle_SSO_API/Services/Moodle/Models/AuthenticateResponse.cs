namespace Moodle_SSO_API.Services.Moodle.Models
{
    public class AuthenticateResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Privatetoken { get; set; } = string.Empty;
        public string Errorcode { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public bool Success { get; set; }
        public GetUserByEmailResponse? UserData { get; set; }
    }
} 