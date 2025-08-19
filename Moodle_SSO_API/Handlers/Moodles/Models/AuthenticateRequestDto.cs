using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Handlers.Moodles.Models
{
    public class AuthenticateRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public Enterprise Enterprise { get; set; }
    }
} 