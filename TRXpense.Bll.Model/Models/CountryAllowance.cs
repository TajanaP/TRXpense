using System.ComponentModel.DataAnnotations;

namespace TRXpense.Bll.Model
{
    public class CountryAllowance
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }
    }
}
