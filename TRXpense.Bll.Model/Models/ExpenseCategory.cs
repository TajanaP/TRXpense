using System.ComponentModel.DataAnnotations;

namespace TRXpense.Bll.Model
{
    public class ExpenseCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Account { get; set; }
    }
}
