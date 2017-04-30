using Microsoft.AspNet.Identity;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.Mappers;
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

        public TravelReportController()
        {
            _travelReportRepository = new TravelReportRepository();
            _countryAllowanceRepository = new CountryAllowanceRepository();
            _vehicleRepository = new VehicleRepository();
            _applicationUserRepository = new ApplicationUserRepository();
        }

        // GET: TravelReport
        public ActionResult Index(int? page, string query = null)
        {
            var travelReports = _travelReportRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews();

            foreach (var report in travelReports)
            {
                report.Employee = _applicationUserRepository.FindById(report.EmployeeId).MapToView();
                report.Country = _countryAllowanceRepository.FindById(report.CountryAllowanceId).MapToView();
            }

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
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            FillDropDownValuesForCountry();
            FillDropDownValuesForVehicleRegistration();

            var travelReport = _travelReportRepository.FindById(id).MapToView();

            // auto-fill Employee
            var userId = User.Identity.GetUserId();
            var userInDb = _applicationUserRepository.FindById(userId).MapToView();
            ViewBag.User = userInDb.FirstName + " " + userInDb.LastName;

            // auto-fill Daily Allowance
            var allowance = _countryAllowanceRepository.FindById(travelReport.CountryAllowanceId);
            ViewBag.Allowance = allowance.Amount + " " + allowance.Currency;

            return View(travelReport);
        }

        [HttpPost]
        public ActionResult Edit(TravelReportVM view)
        {
            if (ModelState.IsValid)
            {
                view.EmployeeId = User.Identity.GetUserId();
                _travelReportRepository.UpdateInDatabase(view.MapToModel(), view.Id);
                _travelReportRepository.Save();
            }

            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var travelReportInDB = _travelReportRepository.FindById(id);
            bool result = false;

            if (travelReportInDB != null)
            {
                _travelReportRepository.DeleteFromDatabase(travelReportInDB);
                _travelReportRepository.Save();
                result = true;
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllowanceForCountry(int countryId)
        {
            var allowance = _countryAllowanceRepository.FindById(countryId).MapToView();
            var result = allowance.Amount + " " + allowance.Currency;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void FillDropDownValuesForCountry()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "-- Select Country --";
            listItem.Value = "";
            selectItems.Add(listItem);

            selectItems.AddRange(_countryAllowanceRepository.GetAllFromDatabaseEnumerable().ToList().MapToListCountries());
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
    }
}