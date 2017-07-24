﻿using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.Mappers;
using TRXpense.App.Web.Services;
using TRXpense.App.Web.ViewModels;
using TRXpense.Dal.Repositories;

namespace TRXpense.App.Web.Controllers
{
    public class TravelReportController : Controller
    {
        private readonly ITravelReportRepository _travelReportRepository;
        private readonly ICountryAllowanceRepository _countryAllowanceRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IExpenseRepository _expenseRepository;

        public TravelReportController()
        {
            _travelReportRepository = new TravelReportRepository();
            _countryAllowanceRepository = new CountryAllowanceRepository();
            _vehicleRepository = new VehicleRepository();
            _applicationUserRepository = new ApplicationUserRepository();
            _expenseCategoryRepository = new ExpenseCategoryRepository();
            _expenseRepository = new ExpenseRepository();
        }

        // GET: TravelReport
        public ActionResult Index(int? page, string query = null)
        {
            var travelReports = _travelReportRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews();

            foreach (var report in travelReports) // get fixed values for TravelReport Index Table
            {
                report.Employee = _applicationUserRepository.FindById(report.EmployeeId).MapToView();
                report.Country = _countryAllowanceRepository.FindById(report.CountryAllowanceId).MapToView();
            }

            if (TempData["travelReportCreatedMessage"] != null)
                ViewBag.SuccessMessage = TempData["travelReportCreatedMessage"].ToString();
            if (TempData["travelReportDeletedMessage"] != null)
                ViewBag.SuccessMessage = TempData["travelReportDeletedMessage"].ToString();
            if (TempData["travelReportErrorMessage"] != null)
                ViewBag.FailureMessage = TempData["travelReportErrorMessage"].ToString();

            // paging
            int pageSize = 10;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfTravelReports = travelReports.ToPagedList(pageNumber, pageSize); // will only contain 10 products max because of the pageSize

            // searching
            if (!string.IsNullOrEmpty(query))
            {
                //int i;
                var travelReportSearched = travelReports
                    .Where(m => m.Employee.FirstName.ToLower().Contains(query.ToLower())
                    || m.Employee.LastName.ToLower().Contains(query.ToLower())
                    || m.Country.Country.ToLower().Contains(query.ToLower()));
                //    || v.Departure.Equals(int.TryParse(query, out i) ? i : (int?)null)
                //    || v.Return.Equals(int.TryParse(query, out i) ? i : (int?)null))

                onePageOfTravelReports = travelReportSearched.ToPagedList(pageNumber, pageSize);
            }

            ViewBag.onePageOfTravelReports = onePageOfTravelReports;
            return View(onePageOfTravelReports);
        }

        public ActionResult Details(int id)
        {
            var travelReport = _travelReportRepository.FindById(id).MapToView();

            return PartialView("_Details", travelReport);
        }

        public ActionResult Create()
        {
            FillDropDownValuesForCountry();
            FillDropDownValuesForVehicleRegistration();

            // auto-fill Employee
            var userId = User.Identity.GetUserId();
            var userInDb = _applicationUserRepository.FindById(userId).MapToView();
            ViewBag.User = userInDb.FirstName + " " + userInDb.LastName;

            return View();
        }

        [HttpPost]
        public ActionResult Create(TravelReportVM view)
        {
            ModelState.Remove("Id");
            view.EmployeeId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                _travelReportRepository.AddToDatabase(view.MapToModel());
                _travelReportRepository.Save();
                TempData["travelReportCreatedMessage"] = "Travel report successfully created!";

                // add Id to view after new report is generated
                var latestId = _travelReportRepository.GetAllFromDatabaseEnumerable().Max(t => t.Id);
                view.Id = latestId;

                CalculateAllowanceExpenses(view);
                CalculateMileageExpenses(view);
                CalculateExpenseSum(view);
            }
            else
            {
                TempData["travelReportErrorMessage"] = "Something went wrong, please try again!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var travelReport = _travelReportRepository.FindById(id).MapToView();

            FillDropDownValuesForCountry();
            FillDropDownValuesForVehicleRegistration();
            GetFixedValuesForTravelReportEditFormAndExpenseIndexTable(travelReport);

            if (TempData["expenseCreatedOrUpdatedMessage"] != null)
                ViewBag.Message = TempData["expenseCreatedOrUpdatedMessage"].ToString();
            if (TempData["expenseDeletedMessage"] != null)
                ViewBag.Message = TempData["expenseDeletedMessage"].ToString();
            if (TempData["travelReportUpdatedMessage"] != null)
                ViewBag.Message = TempData["travelReportUpdatedMessage"].ToString();

            return View(travelReport);
        }

        [HttpPost]
        public ActionResult Edit(TravelReportVM view)
        {
            view.EmployeeId = User.Identity.GetUserId();

            CalculateAllowanceExpenses(view);
            CalculateMileageExpenses(view);
            CalculateExpenseSum(view);

            if (ModelState.IsValid)
            {
                _travelReportRepository.UpdateInDatabase(view.MapToModel(), view.Id);
                _travelReportRepository.Save();
                TempData["travelReportUpdatedMessage"] = "Travel report successfully updated!";
            }

            return RedirectToAction("Edit/" + view.Id);
        }

        public JsonResult Delete(int id)
        {
            var travelReportInDB = _travelReportRepository.FindById(id);
            bool result = false;

            if (travelReportInDB != null)
            {
                _travelReportRepository.DeleteFromDatabase(travelReportInDB);
                _travelReportRepository.Save();
                TempData["travelReportDeletedMessage"] = "Travel report successfully deleted!";
                result = true;
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // EXPENSE

        public ActionResult CreateExpense(int travelReportId)
        {
            FillDropDownValuesForOfficialCurrency();
            FillDropDownValuesForExpenseCategory();

            var expense = new ExpenseVM
            {
                TravelReportId = travelReportId
            };

            return PartialView("_CreateExpense", expense);
        }

        public ActionResult EditExpense(int id)
        {
            var expense = _expenseRepository.FindById(id).MapToView();
            FillDropDownValuesForOfficialCurrency();
            FillDropDownValuesForExpenseCategory();

            // Allowance and Private Car Transportation can NOT be edited
            if (expense.ExpenseCategoryId == 1)
            {
                TempData["allowanceExpenseCanNotBeEditedMessage"] = "Allowance expense is calculated from departure/return dates, you can not edit it here!";
                ViewBag.WarningMessage = TempData["allowanceExpenseCanNotBeEditedMessage"].ToString();
                return PartialView("_InfoMessage");
            }
            else if (expense.ExpenseCategoryId == 10)
            {
                TempData["privateCarExpenseCanNotBeEditedMessage"] = "Private Car expense is calculated from start/end mileage, you can not edit it here!";
                ViewBag.WarningMessage = TempData["privateCarExpenseCanNotBeEditedMessage"].ToString();
                return PartialView("_InfoMessage");
            }
            else
                return PartialView("_EditExpense", expense);
        }

        [HttpPost]
        public ActionResult SaveExpense(ExpenseVM view)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (view.Id == 0)
                {
                    _expenseRepository.AddToDatabase(view.MapToModel());
                    _expenseRepository.Save();
                    TempData["expenseCreatedOrUpdatedMessage"] = "Expense successfully created!";
                }
                else
                {
                    _expenseRepository.UpdateInDatabase(view.MapToModel(), view.Id);
                    _expenseRepository.Save();
                    TempData["expenseCreatedOrUpdatedMessage"] = "Expense successfully updated!";
                }
            }

            // calculate ExchangeRate for ExpenseSum if neccessary
            var travelReport = _travelReportRepository.FindById(view.TravelReportId).MapToView();
            var country = _countryAllowanceRepository
                .GetAllFromDatabaseEnumerable()
                .Where(c => c.Id == travelReport.CountryAllowanceId)
                .SingleOrDefault()
                .MapToView();

            travelReport.ExpenseSum = 0;
            if (country.OfficialCurrency != "HRK")
            {
                view.OfficialCurrency = _countryAllowanceRepository.FindById(int.Parse(view.OfficialCurrency)).OfficialCurrency;
                if (view.OfficialCurrency != "HRK")
                    view.BillAmount = CalculateExchangeRate(country.OfficialCurrency, view.Date, view.BillAmount);
            }
            CalculateExpenseSum(travelReport);

            return RedirectToAction("Edit/" + view.TravelReportId);
        }

        public JsonResult DeleteExpense(int id)
        {
            var expenseIdDb = _expenseRepository.FindById(id);
            bool result = false;

            if (expenseIdDb != null)
            {
                var expense = _expenseRepository.FindById(id).MapToView();
                var travelReport = _travelReportRepository.GetAllFromDatabaseEnumerable().Where(t => t.Id == expense.TravelReportId).SingleOrDefault().MapToView();

                if (expense.ExpenseCategoryId != 1 && expense.ExpenseCategoryId != 10) // Allowance and Private Car Transportation can NOT be deleted
                {
                    _expenseRepository.DeleteFromDatabase(expenseIdDb);
                    _expenseRepository.Save();
                    TempData["expenseDeletedMessage"] = "Expense successfully deleted!";
                    result = true;

                    travelReport.ExpenseSum = 0;
                    CalculateExpenseSum(travelReport);
                }
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // HELPER METHODS

        public JsonResult GetAllowanceForCountry(int countryId)
        {
            var allowance = _countryAllowanceRepository.FindById(countryId).MapToView();
            var result = allowance.Amount + " " + allowance.AllowanceCurrency;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void FillDropDownValuesForCountry()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "-- Select Country --";
            listItem.Value = "";
            selectItems.Add(listItem);

            selectItems.AddRange(_countryAllowanceRepository.GetAllFromDatabaseEnumerable().ToList().MapToListCountries().OrderBy(o => o.Text));
            ViewBag.Countries = selectItems;
        }

        private void FillDropDownValuesForVehicleRegistration()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "-- Select Car Registration --";
            listItem.Value = "";
            selectItems.Add(listItem);

            selectItems.AddRange(_vehicleRepository.GetAllFromDatabaseEnumerable().ToList().MapToListVehicles());
            ViewBag.Vehicles = selectItems;
        }

        private void FillDropDownValuesForOfficialCurrency(/*int id*/)
        {
            var selectItems = new List<SelectListItem>();
            var listItem = new SelectListItem();
            listItem.Text = "-- Select Currency --";
            listItem.Value = "";
            selectItems.Add(listItem);

            selectItems.AddRange(_countryAllowanceRepository.GetAllFromDatabaseEnumerable().ToList().MapToListCurrencies().OrderBy(o => o.Text));
            var selectItemsDistinct = selectItems.GroupBy(g => g.Text).Select(g => g.First());

            // OPTION 2. --> getting only HRK and currency of a choosen country in dropdown

            //var travelReport = _travelReportRepository.FindById(id).MapToView();
            //var countryAllowances = _countryAllowanceRepository.GetAllFromDatabaseEnumerable();
            //var country = countryAllowances.Where(c => c.Id == travelReport.CountryAllowanceId).SingleOrDefault().MapToView();
            //var hrk = countryAllowances.Where(c => c.OfficialCurrency == "HRK").SingleOrDefault().MapToView();

            //var officialCurrency = new SelectListItem();
            //officialCurrency.Text = country.OfficialCurrency;
            //officialCurrency.Value = country.Id.ToString();
            //selectItems.Add(officialCurrency);

            //if (country.OfficialCurrency != "HRK") // HRK must be available for every expense
            //{
            //    var croatianCurrency = new SelectListItem();
            //    croatianCurrency.Text = hrk.OfficialCurrency;
            //    croatianCurrency.Value = hrk.Id.ToString();
            //    selectItems.Add(croatianCurrency);
            //}

            ViewBag.OfficialCurrency = selectItemsDistinct.ToList();
        }

        private void FillDropDownValuesForExpenseCategory()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "-- Select Category --";
            listItem.Value = "";
            selectItems.Add(listItem);

            selectItems.AddRange(_expenseCategoryRepository.GetAllFromDatabaseEnumerable().ToList().MapToListExpenseCategories());

            // remove category that is calculated by program itself
            selectItems.Remove(selectItems.Where(i => i.Value == "1").Single());    // allowance
            selectItems.Remove(selectItems.Where(i => i.Value == "10").Single());   // private car transportation

            ViewBag.Categories = selectItems;
        }

        private void GetFixedValuesForTravelReportEditFormAndExpenseIndexTable(TravelReportVM travelReport)
        {
            var expenses = travelReport.Expenses.Where(e => e.TravelReportId == travelReport.Id).ToList();

            // get expenseCategory for ExpenseIndexTable and allways show Allowance currency and PrivateCarTransportation currency as "HRK"
            foreach (var expense in expenses)
            {
                expense.ExpenseCategory = _expenseCategoryRepository.FindById(expense.ExpenseCategoryId).MapToView();

                if (expense.ExpenseCategoryId == 1 || expense.ExpenseCategoryId == 10) // allowance or private car transportation
                    expense.OfficialCurrency = "HRK";
                else
                    expense.OfficialCurrency = _countryAllowanceRepository.FindById(int.Parse(expense.OfficialCurrency)).OfficialCurrency; // not a foreign key
            }

            // auto-fill Employee
            var userId = User.Identity.GetUserId();
            var userInDb = _applicationUserRepository.FindById(userId).MapToView();
            ViewBag.User = userInDb.FirstName + " " + userInDb.LastName;

            // auto-fill DailyAllowance
            var allowance = _countryAllowanceRepository.FindById(travelReport.CountryAllowanceId).MapToView();
            ViewBag.Allowance = allowance.Amount + " " + allowance.AllowanceCurrency;
        }

        private void CalculateExpenseSum(TravelReportVM travelReport)
        {
            var expenses = _expenseRepository.GetAllFromDatabaseEnumerable().Where(t => t.TravelReportId == travelReport.Id).ToList();
            foreach (var expense in expenses)
                travelReport.ExpenseSum = travelReport.ExpenseSum + expense.BillAmount;

            _travelReportRepository.UpdateInDatabase(travelReport.MapToModel(), travelReport.Id);
            _travelReportRepository.Save();
        }

        private void CalculateAllowanceExpenses(TravelReportVM view)
        {
            var countryAllowance = _countryAllowanceRepository.FindById(view.CountryAllowanceId).MapToView();
            var hours = (view.Return - view.Departure).TotalHours;
            decimal allowanceSum = 0;
            view.NumberOfHours = hours;

            if (hours < 8)
            {
                allowanceSum = 0;
            }
            else if (hours >= 8 && hours < 12)
            {
                allowanceSum = (countryAllowance.Amount / 2);
                view.NumberOfAllowances = 0.5;
            }
            else
            {
                for (double i = hours; i >= 12; i -= 12)
                {
                    allowanceSum += countryAllowance.Amount / 2;
                    view.NumberOfAllowances += 0.5;
                }
            }

            if (countryAllowance.AllowanceCurrency != "HRK")
                allowanceSum = CalculateExchangeRate(countryAllowance.AllowanceCurrency, view.Return, allowanceSum);

            // add/update allowance in Expenses

            var allowanceExpense = new ExpenseVM // create expense
            {
                TravelReportId = view.Id,
                Date = view.Return,
                ExpenseCategoryId = 1,
                BillNumber = "-",
                BillAmount = allowanceSum,
                OfficialCurrency = "HRK"
            };

            var expenses = _expenseRepository.GetAllFromDatabaseEnumerable().Where(e => e.TravelReportId == view.Id).ToList();
            bool exists = false;

            foreach (var expense in expenses) // check if it already exists, if NO -> add, if YES -> update
            {
                if (expense.ExpenseCategoryId == 1)
                {
                    exists = true;
                    allowanceExpense.Id = expense.Id;
                }
            }

            if (exists)
                _expenseRepository.UpdateInDatabase(allowanceExpense.MapToModel(), allowanceExpense.Id);
            else
                _expenseRepository.AddToDatabase(allowanceExpense.MapToModel());

            _expenseRepository.Save();
        }

        private void CalculateMileageExpenses(TravelReportVM view)
        {
            decimal mileageSum = 0;
            var countryAllowance = _countryAllowanceRepository.FindById(view.CountryAllowanceId).MapToView();

            if (view.StartMileage != null && view.EndMileage != null)
                mileageSum = view.EndMileage.Value - view.StartMileage.Value;

            // add/update private car expense in Expenses

            if (view.VehicleType.Value == 0)
            {
                var mileageExpense = new ExpenseVM
                {
                    TravelReportId = view.Id,
                    Date = view.Return,
                    ExpenseCategoryId = 10,
                    BillNumber = "-",
                    BillAmount = mileageSum * 2,
                    OfficialCurrency = "HRK"
                };

                var expenses = _expenseRepository.GetAllFromDatabaseEnumerable().Where(e => e.TravelReportId == view.Id).ToList();
                bool exists = false;

                foreach (var expense in expenses)
                {
                    if (expense.ExpenseCategoryId == 10)
                    {
                        exists = true;
                        mileageExpense.Id = expense.Id;
                    }
                }

                if (exists)
                    _expenseRepository.UpdateInDatabase(mileageExpense.MapToModel(), mileageExpense.Id);
                else
                    _expenseRepository.AddToDatabase(mileageExpense.MapToModel());

                _expenseRepository.Save();
            }
        }

        private decimal CalculateExchangeRate(string currency, DateTime date, decimal amount)
        {
            decimal finalAmount = 0;

            switch (currency)
            {
                case "BAM":
                    finalAmount = amount * decimal.Parse(3.75.ToString());
                    break;
                case "BGN":
                    finalAmount = amount * decimal.Parse(3.75.ToString());
                    break;
                case "RON":
                    finalAmount = amount * decimal.Parse(1.60.ToString());
                    break;
                case "RSD":
                    finalAmount = amount * decimal.Parse(0.06.ToString());
                    break;
                case "TRY":
                    finalAmount = amount * decimal.Parse(1.80.ToString());
                    break;
                default:
                    var exchangeRateList = new ExchangeRate(date.ToString("yyyy-MM-dd"));
                    ExchangeRate.item exchangeRate = exchangeRateList.getExchangeRate(currency);
                    string excRate = exchangeRate.srednji_tecaj.Replace(",", ".");
                    finalAmount = amount * (decimal.Parse(exchangeRate.jedinica) * decimal.Parse(excRate));
                    break;
            }
            return finalAmount;
        }
    }
}