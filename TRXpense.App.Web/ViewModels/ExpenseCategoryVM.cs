using System.ComponentModel.DataAnnotations;

namespace TRXpense.App.Web.ViewModels
{
    public class ExpenseCategoryVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Account*")]
        public int Account { get; set; }
    }
}