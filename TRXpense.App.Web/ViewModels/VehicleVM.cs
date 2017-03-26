using System.ComponentModel.DataAnnotations;

namespace TRXpense.App.Web.ViewModels
{
    public class VehicleVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Brand *")]
        public string Brand { get; set; }

        public string Model { get; set; }

        [Display(Name = "Production Year")]
        public int ProductionYear { get; set; }

        [Required]
        [Display(Name = "Registration *")]
        public string Registration { get; set; }
    }
}