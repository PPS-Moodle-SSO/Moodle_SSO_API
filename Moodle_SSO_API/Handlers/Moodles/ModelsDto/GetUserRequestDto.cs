using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Handlers.Moodles.Models
{
    public class GetUserRequestDto
    {
        public string UserEmail { get; set; }
        public Enterprise Enterprise { get; set; }
    }
}
