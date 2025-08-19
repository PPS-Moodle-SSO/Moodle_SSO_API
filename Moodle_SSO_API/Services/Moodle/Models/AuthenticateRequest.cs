namespace Moodle_SSO_API.Services.Moodle.Models
{
    public class AuthenticateRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Service { get; set; } = "moodle_mobile_app";
    }
} 