using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;

namespace TRXpense.App.Web.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseVM MapToView(this Expense model)
        {
            if (model == null)
                return null;

            return new ExpenseVM
            {
                Id = model.Id,
                Date = model.Date,
                BillNumber = model.BillNumber,
                BillAmount = model.BillAmount,
                OfficialCurrency = model.Currency,
                ExpenseCategoryId = model.ExpenseCategoryId,
                ExpenseCategory = model.ExpenseCategory.MapToView(),
                TravelReportId = model.TravelReportId
            };
        }

        public static List<ExpenseVM> MapToViews(this IQueryable<Expense> model)
        {
            if (model == null)
                return null;

            List<ExpenseVM> result = new List<ExpenseVM>();
            //var result = new List<ExpenseVM>();

            foreach (var item in model)
            {
                result.Add(item.MapToView());
            }

            return result;
        }

        public static Expense MapToModel(this ExpenseVM view)
        {
            if (view == null)
                return null;

            return new Expense
            {
                Id = view.Id,
                Date = view.Date,
                BillNumber = view.BillNumber,
                BillAmount = view.BillAmount,
                Currency = view.OfficialCurrency,
                ExpenseCategoryId = view.ExpenseCategoryId,
                ExpenseCategory = view.ExpenseCategory.MapToModel(),
                TravelReportId = view.TravelReportId
            };
        }

        public static SelectListItem MapToListExpenseCategory(this ExpenseCategory model)
        {
            if (model == null)
                return null;

            return new SelectListItem()
            {
                Text = model.Account + " - " + model.Name,
                Value = model.Id.ToString()
            };
        }

        public static List<SelectListItem> MapToListExpenseCategories(this List<ExpenseCategory> model)
        {
            if (model == null)
                return null;

            var result = new List<SelectListItem>();
            foreach (var item in model)
            {
                result.Add(item.MapToListExpenseCategory());
            }

            return result;
        }

        public static SelectListItem MapToListCurrency(this CountryAllowance model)
        {
            if (model == null)
                return null;

            return new SelectListItem()
            {
                Text = model.OfficialCurrency,
                Value = model.Id.ToString()
            };
        }

        public static List<SelectListItem> MapToListCurrencies(this List<CountryAllowance> model)
        {
            if (model == null)
                return null;

            var result = new List<SelectListItem>();
            foreach (var item in model)
            {
                result.Add(item.MapToListCurrency());
            }

            return result;
        }
    }
}