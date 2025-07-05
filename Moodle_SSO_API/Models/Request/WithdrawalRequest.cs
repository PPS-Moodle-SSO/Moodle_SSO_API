using System.ComponentModel.DataAnnotations;

namespace Moodle_SSO_API.Models.Request
{
    public class WithdrawalRequest
    {
        [Required]
        public int bank_card_id { get; set; }
        [Required]
        public double withdrawal_amount { get; set; }
    }
}
