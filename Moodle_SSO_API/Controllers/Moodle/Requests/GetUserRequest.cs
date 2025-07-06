using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.Controllers.Moodle.Requests
{
    public class GetUserRequest
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Domain { get; set; }
    }
}
