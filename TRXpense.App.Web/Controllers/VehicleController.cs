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

        public VehicleController()
        {
            _vehicleRepository = new VehicleRepository();
        }

        // GET: Vehicle
        public ActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews();

            return View(vehicles);
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
        public ActionResult Save(VehicleVM vehicle)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            if (vehicle.Id == 0)
                _vehicleRepository.AddToDatabase(vehicle.MapToModel());
            else
                _vehicleRepository.UpdateInDatabase(vehicle.MapToModel(), vehicle.Id);

            _vehicleRepository.Save();

            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var vehicleInDB = _vehicleRepository.FindById(id);
            //var vehicles = _vehicleRepository.GetAllFromDatabaseEnumerable().Where(u => u.Id == id).ToList();
            bool result = false;

            if (/*vehicles.Count == 0 && */vehicleInDB != null)
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