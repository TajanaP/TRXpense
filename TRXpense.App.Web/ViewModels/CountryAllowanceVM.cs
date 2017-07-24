using System.ComponentModel.DataAnnotations;

namespace TRXpense.App.Web.ViewModels
{
    public class CountryAllowanceVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Country*")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Amount*")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Allowance Currency*")]
        public string AllowanceCurrency { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Official Currency*")]
        public string OfficialCurrency { get; set; }
    }
}