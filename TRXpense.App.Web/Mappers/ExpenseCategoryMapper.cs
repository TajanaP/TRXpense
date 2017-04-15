using System.Collections.Generic;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class ExpenseCategoryMapper
    {
        public static ExpenseCategoryVM MapToView(this ExpenseCategory model)
        {
            if (model == null)
                return null;

            return new ExpenseCategoryVM
            {
                Id = model.Id,
                Name = model.Name,
                Account = model.Account
            };
        }

        public static List<ExpenseCategoryVM> MapToViews(this List<ExpenseCategory> model)
        {
            if (model == null)
                return null;

            var result = new List<ExpenseCategoryVM>();

            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        public static ExpenseCategory MapToModel(this ExpenseCategoryVM view)
        {
            if (view == null)
                return null;

            return new ExpenseCategory
            {
                Id = view.Id,
                Name = view.Name,
                Account = view.Account
            };
        }
    }
}