using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.ViewModels
{
    public class TravelReportVM
    {
        public int Id { get; set; }

        //[Required]
        //[Display(Name = "Date")]
        //public DateTime DateCreated { get; set; } // popunjava se sa DateTime.Now

        //[Required]
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; } // foreign key
        public RegisterViewModel Employee { get; set; }
        //public IEnumerable<RegisterViewModel> Employees { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryAllowanceId { get; set; } // foreign key
        public CountryAllowanceVM Country { get; set; }
        //public IEnumerable<CountryAllowanceVM> Countries { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Daily Allowance")]
        public decimal DailyAllowance { get; set; } // polje koje se povlači automatski prema odabranoj državi

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Return { get; set; }

        [Required]
        [Display(Name = "Hours")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double NumberOfHours { get; set; }

        [Required]
        [Display(Name = "Allowances")]
        public double NumberOfAllowances { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        public VehicleType? VehicleType { get; set; }

        [Display(Name = "Company Car")]
        public int? VehicleId { get; set; } // foreign key, nullable, hidden, otvara se samo ako je odabran VehicleType - CompanyCar
        //public VehicleVM CompanyVehicle { get; set; }
        //public IEnumerable<VehicleVM> Vehicles { get; set; }

        [Display(Name = "Start Mileage")]
        public decimal? StartMileage { get; set; }

        [Display(Name = "End Mileage")]
        public decimal? EndMileage { get; set; }

        [Display(Name = "Reason for Travel")]
        public string ReasonForTravel { get; set; }

        [Required]
        [Display(Name = "Expense Total")]
        public decimal ExpenseSum { get; set; } // ovo polje se samo popunjava na temelju svih unešenih expensa

        [Required]
        public Status Status { get; set; }

        public List<ExpenseVM> Expenses { get; set; }
    }
}