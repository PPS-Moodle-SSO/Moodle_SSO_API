using Moodle_SSO_API.Handlers.Moodles.Models;

namespace Moodle_SSO_API.Services.Moodle.Models
{
    public class GetUserByEmailResponse
    {
        public List<GetUserResponseDto> Users { get; set; }
    }

}
