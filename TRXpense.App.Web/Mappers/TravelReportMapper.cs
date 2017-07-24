using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class TravelReportMapper
    {
        public static TravelReportVM MapToView(this TravelReport model)
        {
            if (model == null)
                return null;

            IQueryable<Expense> expenses = model.Expenses.AsQueryable();

            return new TravelReportVM
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                //Employee = model.Employee.MapToView(),
                CountryAllowanceId = model.CountryAllowanceId,
                //Country = model.Country.MapToView(),
                City = model.City,
                Departure = model.Departure,
                Return = model.Return,
                NumberOfHours = model.NumberOfHours,
                NumberOfAllowances = model.NumberOfAllowances,
                VehicleType = model.VehicleType,
                VehicleId = model.VehicleId,
                StartMileage = model.StartMileage,
                EndMileage = model.EndMileage,
                //Vehicle = model.Vehicle,
                //CompanyVehicle = model.CompanyVehicle.MapToView(),
                ReasonForTravel = model.ReasonForTravel,
                ExpenseSum = model.ExpenseSum,
                Status = model.Status,
                Expenses = expenses.MapToViews()
            };
        }

        public static List<TravelReportVM> MapToViews(this List<TravelReport> model)
        {
            if (model == null)
                return null;

            var result = new List<TravelReportVM>();

            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        public static TravelReport MapToModel(this TravelReportVM view)
        {
            if (view == null)
                return null;

            ICollection<Expense> expenses = new List<Expense>();

            if (view.Expenses != null)
            {
                foreach (var item in view.Expenses)
                {
                    expenses.Add(item.MapToModel());
                }
            }

            return new TravelReport
            {
                Id = view.Id,
                EmployeeId = view.EmployeeId,
                //Employee = view.Employee.MapToModel(),
                CountryAllowanceId = view.CountryAllowanceId,
                //Country = view.Country.MapToModel(),
                City = view.City,
                Departure = view.Departure,
                Return = view.Return,
                NumberOfHours = view.NumberOfHours,
                NumberOfAllowances = view.NumberOfAllowances,
                VehicleType = view.VehicleType,
                VehicleId = view.VehicleId,
                StartMileage = view.StartMileage,
                EndMileage = view.EndMileage,
                //Vehicle = view.Vehicle,
                //CompanyVehicle = view.CompanyVehicle.MapToModel(),
                ReasonForTravel = view.ReasonForTravel,
                ExpenseSum = view.ExpenseSum,
                Status = view.Status,
                Expenses = expenses
            };
        }

        public static SelectListItem MapToListCountry(this CountryAllowance model)
        {
            if (model == null)
                return null;

            return new SelectListItem()
            {
                Text = model.Country,
                Value = model.Id.ToString()
            };
        }

        public static List<SelectListItem> MapToListCountries(this List<CountryAllowance> model)
        {
            if (model == null)
                return null;

            var result = new List<SelectListItem>();
            foreach (var item in model)
            {
                result.Add(item.MapToListCountry());
            }

            return result;
        }

        public static SelectListItem MapToListVehicle(this Vehicle model)
        {
            if (model == null)
                return null;

            return new SelectListItem()
            {
                Text = model.Registration,
                Value = model.Id.ToString()
            };
        }

        public static List<SelectListItem> MapToListVehicles(this List<Vehicle> model)
        {
            if (model == null)
                return null;

            var result = new List<SelectListItem>();
            foreach (var item in model)
            {
                result.Add(item.MapToListVehicle());
            }

            return result;
        }
    }
}