using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRXpense.Bll.Model
{
    public class CostCenter
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name *")]
        [StringLength(8)]
        public string Name { get; set; }

        [Required, Display(Name = "Description *")]
        public string Description { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
