using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.Models
{
    public class Enterprise
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEnterprise { get; set; }

        [Required]
        public string NameEnterprise { get; set; }

        [Required]
        public string IdOrganizationDefault { get; set; }

        [Required]
        public string Domain { get; set; }
        
        [Required]
        public string MoodleUrl { get; set; }
        
        [Required]
        public string MoodleApiKey { get; set; }
    }
}
