using System.Collections.Generic;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class CostCenterMapper
    {
        public static CostCenterVM MapToView(this CostCenter model)
        {
            if (model == null)
                return null;

            return new CostCenterVM
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static List<CostCenterVM> MapToViews(this List<CostCenter> model)
        {
            if (model == null)
                return null;

            var result = new List<CostCenterVM>();
            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        public static CostCenter MapToModel(this CostCenterVM view)
        {
            if (view == null)
                return null;

            return new CostCenter
            {
                Id = view.Id,
                Name = view.Name,
                Description = view.Description
            };
        }
    }
}