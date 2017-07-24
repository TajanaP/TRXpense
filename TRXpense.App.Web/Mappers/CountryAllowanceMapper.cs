using System.Collections.Generic;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class CountryAllowanceMapper
    {
        public static CountryAllowanceVM MapToView(this CountryAllowance model)
        {
            if (model == null)
                return null;

            return new CountryAllowanceVM
            {
                Id = model.Id,
                Country = model.Country,
                Amount = model.Amount,
                AllowanceCurrency = model.AllowanceCurrency,
                OfficialCurrency = model.OfficialCurrency
            };
        }

        public static List<CountryAllowanceVM> MapToViews(this List<CountryAllowance> model)
        {
            if (model == null)
                return null;

            var result = new List<CountryAllowanceVM>();

            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        public static CountryAllowance MapToModel(this CountryAllowanceVM view)
        {
            if (view == null)
                return null;

            return new CountryAllowance
            {
                Id = view.Id,
                Country = view.Country,
                Amount = view.Amount,
                AllowanceCurrency = view.AllowanceCurrency,
                OfficialCurrency = view.OfficialCurrency
            };
        }
    }
}