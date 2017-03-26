using System.Collections.Generic;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class VehicleMapper
    {
        public static VehicleVM MapToView(this Vehicle model)
        {
            if (model == null)
                return null;

            return new VehicleVM
            {
                Id = model.Id,
                Brand = model.Brand,
                Model = model.Model,
                ProductionYear = model.ProductionYear,
                Registration = model.Registration
            };
        }

        public static List<VehicleVM> MapToViews(this List<Vehicle> model)
        {
            if (model == null)
                return null;

            var result = new List<VehicleVM>();
            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        public static Vehicle MapToModel(this VehicleVM view)
        {
            if (view == null)
                return null;

            return new Vehicle
            {
                Id = view.Id,
                Brand = view.Brand,
                Model = view.Model,
                ProductionYear = view.ProductionYear,
                Registration = view.Registration
            };
        }
    }
}