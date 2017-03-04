using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRXpense.Bll.Model
{
    public class CostCenter
    {
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
