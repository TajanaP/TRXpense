using PagedList;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.Mappers;
using TRXpense.App.Web.ViewModels;
using TRXpense.Dal.Repositories;

namespace TRXpense.App.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITravelReportRepository _travelReportRepository;

        public VehicleController()
        {
            _vehicleRepository = new VehicleRepository();
            _travelReportRepository = new TravelReportRepository();
        }

        // GET: Vehicle
        public ActionResult Index(int? page, string query = null)
        {
            var vehicles = _vehicleRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews().OrderBy(o => o.Brand);

            // paging
            int pageSize = 5;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfVehicles = vehicles.ToPagedList(pageNumber, pageSize); // will only contain 5 items max because of the pageSize

            // searching
            if (!string.IsNullOrEmpty(query))
            {
                int i;
                var vehicleSearched = _vehicleRepository.GetAllFromDatabaseEnumerable()
                    .Where(v => v.Brand.ToLower().Contains(query.ToLower())
                        || v.Model.ToLower().Contains(query.ToLower())
                        || v.Registration.ToLower().Contains(query.ToLower())
                        || v.ProductionYear.Equals(int.TryParse(query, out i) ? i : (int?)null))
                    .ToList()
                    .MapToViews();

                onePageOfVehicles = vehicleSearched.ToPagedList(pageNumber, pageSize);
            }

            ViewBag.onePageOfVehicles = onePageOfVehicles;
            return View(onePageOfVehicles);
        }

        public ActionResult Create()
        {
            return PartialView("_VehicleForm");
        }

        public ActionResult Edit(int id)
        {
            var vehicle = _vehicleRepository.FindById(id).MapToView();

            if (vehicle == null)
                return HttpNotFound();

            return PartialView("_VehicleForm", vehicle);
        }

        [HttpPost]
        public ActionResult Save(VehicleVM view)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (view.Id == 0)
                    _vehicleRepository.AddToDatabase(view.MapToModel());
                else
                    _vehicleRepository.UpdateInDatabase(view.MapToModel(), view.Id);

                _vehicleRepository.Save();
            }
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var vehicleInDB = _vehicleRepository.FindById(id);
            bool result = false;
            var travelReportsThatUseThisVehicle =
                    _travelReportRepository.GetAllFromDatabaseEnumerable().Where(v => v.VehicleId == id).ToList().MapToViews();

            if (travelReportsThatUseThisVehicle.Count == 0 && vehicleInDB != null)
            {
                _vehicleRepository.DeleteFromDatabase(vehicleInDB);
                _vehicleRepository.Save();
                result = true;
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}