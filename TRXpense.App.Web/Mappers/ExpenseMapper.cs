using System.Collections.Generic;
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
                ExpenseCategoryId = model.ExpenseCategoryId,
                ExpenseCategory = model.ExpenseCategory.MapToView(),
                TravelReportId = model.TravelReportId
            };
        }

        public static List<ExpenseVM> MapToViews(this List<Expense> model)
        {
            if (model == null)
                return null;

            var result = new List<ExpenseVM>();

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
    }
}