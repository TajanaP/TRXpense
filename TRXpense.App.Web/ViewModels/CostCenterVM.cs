using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.ViewModels
{
    public class CostCenterVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name *")]
        [StringLength(8)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description *")]
        public string Description { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}