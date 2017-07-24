using PagedList;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.Mappers;
using TRXpense.App.Web.ViewModels;
using TRXpense.Dal.Repositories;

namespace TRXpense.App.Web.Controllers
{
    public class CountryAllowanceController : Controller
    {
        private readonly ICountryAllowanceRepository _countryAllowanceRepository;

        public CountryAllowanceController()
        {
            _countryAllowanceRepository = new CountryAllowanceRepository();
        }

        // GET: CountryAllowance
        public ActionResult Index(int? page, string query = null)
        {
            var allowances = _countryAllowanceRepository
                .GetAllFromDatabaseEnumerable()
                .ToList()
                .MapToViews()
                .OrderBy(o => o.Country);

            // paging
            int pageSize = 6;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfAllowances = allowances.ToPagedList(pageNumber, pageSize); // will only contain 6 products max because of the pageSize

            // searching
            if (!string.IsNullOrEmpty(query))
            {
                decimal d;
                var allowanceSearched = _countryAllowanceRepository.GetAllFromDatabaseEnumerable()
                    .Where(a => a.Country.ToLower().Contains(query.ToLower())
                        || a.AllowanceCurrency.ToLower().Contains(query.ToLower())
                        || a.Amount.Equals(decimal.TryParse(query, out d) ? d : (decimal?)null))
                    .ToList()
                    .MapToViews();

                onePageOfAllowances = allowanceSearched.ToPagedList(pageNumber, pageSize);
            }

            ViewBag.onePageOfAllowances = onePageOfAllowances;
            return View(onePageOfAllowances);
        }

        public ActionResult Create()
        {
            return PartialView("_CountryAllowanceForm");
        }

        public ActionResult Edit(int id)
        {
            var allowance = _countryAllowanceRepository.FindById(id).MapToView();

            if (allowance == null)
                return HttpNotFound();

            return PartialView("_CountryAllowanceForm", allowance);
        }

        [HttpPost]
        public ActionResult Save(CountryAllowanceVM countryAllowance)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            if (countryAllowance.Id == 0)
                _countryAllowanceRepository.AddToDatabase(countryAllowance.MapToModel());
            else
                _countryAllowanceRepository.UpdateInDatabase(countryAllowance.MapToModel(), countryAllowance.Id);

            _countryAllowanceRepository.Save();

            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var allowanceIdDb = _countryAllowanceRepository.FindById(id);
            bool result = false;

            if (allowanceIdDb != null)
            {
                _countryAllowanceRepository.DeleteFromDatabase(allowanceIdDb);
                _countryAllowanceRepository.Save();
                result = true;
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}