using System.Collections.Generic;
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

            return new TravelReportVM
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                //Employee = model.Employee.MapToView(),
                CountryAllowanceId = model.CountryAllowanceId,
                //Country = model.Country.MapToView(),
                City = model.City,
                VehicleType = model.VehicleType,
                VehicleId = model.VehicleId,
                //Vehicle = model.Vehicle,
                //CompanyVehicle = model.CompanyVehicle.MapToView(),
                ReasonForTravel = model.ReasonForTravel,
                Departure = model.Departure,
                Return = model.Return,
                ExpenseSum = model.ExpenseSum,
                Status = model.Status
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

            return new TravelReport
            {
                Id = view.Id,
                EmployeeId = view.EmployeeId,
                //Employee = view.Employee.MapToModel(),
                CountryAllowanceId = view.CountryAllowanceId,
                //Country = view.Country.MapToModel(),
                City = view.City,
                VehicleType = view.VehicleType,
                VehicleId = view.VehicleId,
                //Vehicle = view.Vehicle,
                //CompanyVehicle = view.CompanyVehicle.MapToModel(),
                ReasonForTravel = view.ReasonForTravel,
                Departure = view.Departure,
                Return = view.Return,
                ExpenseSum = view.ExpenseSum,
                Status = view.Status
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