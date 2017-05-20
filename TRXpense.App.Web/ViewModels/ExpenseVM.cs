using System;
using System.ComponentModel.DataAnnotations;

namespace TRXpense.App.Web.ViewModels
{
    public class ExpenseVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date*")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Bill Number*")]
        public string BillNumber { get; set; }

        [Required]
        [Display(Name = "Bill Amount*")]
        public decimal BillAmount { get; set; }

        [Display(Name = "Expense Category*")]
        public int ExpenseCategoryId { get; set; } // foreign key
        public ExpenseCategoryVM ExpenseCategory { get; set; }

        public int TravelReportId { get; set; } // foreign key
        //public virtual TravelReport TravelReport { get; set; }
    }
}