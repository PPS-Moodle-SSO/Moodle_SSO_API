using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.DTO_s
{
    public class UpdateAccountDTO
    {
        [Required]
        public Int32 id_account { get; set; }

        [Required]
        public Double balance { get; set; }
    }
}
