using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.Controllers.Moodle.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Domain { get; set; } = string.Empty;
    }
} 