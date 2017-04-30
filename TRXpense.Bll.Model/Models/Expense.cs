using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRXpense.Bll.Model
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string BillNumber { get; set; }

        [Required]
        public decimal BillAmount { get; set; }

        public int ExpenseCategoryId { get; set; } // foreign key
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
