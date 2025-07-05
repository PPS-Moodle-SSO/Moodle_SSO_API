using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.Models
{
    public class BankCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_bank_card { get; set; }

        [ForeignKey("id_account")]
        public Account account { get; set; }
    }
}
