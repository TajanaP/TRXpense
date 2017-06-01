using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TRXpense.Bll.Model
{
    public class TravelReport
    {
        public TravelReport()
        {
            Expenses = new List<Expense>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public string EmployeeId { get; set; } // foreign key
        public ApplicationUser Employee { get; set; }

        [Required]
        public int CountryAllowanceId { get; set; } // foreign key
        public CountryAllowance Country { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Return { get; set; }

        [Required]
        public double NumberOfHours { get; set; }

        [Required]
        public double NumberOfAllowances { get; set; }

        [Required]
        public VehicleType? VehicleType { get; set; }

        public int? VehicleId { get; set; } // foreign key, nullable, hidden, otvara se samo ako je odabran VehicleType - CompanyCar
        public Vehicle CompanyVehicle { get; set; }

        public decimal? StartMileage { get; set; }

        public decimal? EndMileage { get; set; }

        public string ReasonForTravel { get; set; }

        [Required]
        public decimal ExpenseSum { get; set; } // ovo polje se samo popunjava na temelju svih unešenih expensa

        [Required]
        public Status Status { get; set; } = Status.Opened;

        public virtual ICollection<Expense> Expenses { get; set; }
    }

    public enum VehicleType
    {
        [Display(Name = "Private Car")]
        PrivateCar,
        [Display(Name = "Company Car")]
        CompanyCar,
        [Display(Name = "Front Seat Passanger")]
        FrontSeatPassenger,
        Plane,
        Bus,
        Train,
        Ferry
    }

    public enum Status
    {
        Opened,
        Closed
    }
}