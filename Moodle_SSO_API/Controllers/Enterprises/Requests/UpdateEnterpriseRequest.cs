using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.Controllers.Enterprises.Requests
{
    public class UpdateEnterpriseRequest
    {
        [Required]
        public int IdEnterprise { get; set; }

        [Required]
        public string MoodleUrl { get; set; }
        
        [Required]
        public string MoodleApiKey { get; set; }
    }
}
