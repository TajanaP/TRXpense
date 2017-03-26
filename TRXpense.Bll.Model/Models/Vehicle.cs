using System.ComponentModel.DataAnnotations;

namespace TRXpense.Bll.Model
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        public string Model { get; set; }

        public int ProductionYear { get; set; }

        [Required]
        [StringLength(15)]
        public string Registration { get; set; }
    }
}
