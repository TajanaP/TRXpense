using System.Linq;
using System.Web.Mvc;
using TRXpense.Bll.Model;
using TRXpense.Dal.Repositories;

namespace TRXpense.App.Web.Controllers
{
    public class CostCenterController : Controller
    {
        private readonly ICostCenterRepository _costCenterRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public CostCenterController()
        {
            _costCenterRepository = new CostCenterRepository();
            _applicationUserRepository = new ApplicationUserRepository();
        }

        // GET: CostCenter
        public ActionResult Index()
        {
            var costCenters = _costCenterRepository.GetAllFromDatabaseEnumerable().ToList();

            return View(costCenters);
        }

        // GET: CostCenter/Details/Id
        public ActionResult Details(int id)
        {
            var users = _applicationUserRepository.GetAllFromDatabaseEnumerable().Where(u => u.CostCenterId == id).ToList();

            return PartialView("_Details", users);
        }

        // GET: /CostCenter/Create
        public ActionResult Create()
        {
            return PartialView("_CostCenterForm");
        }

        // POST: /CostCenter/Create
        [HttpPost]
        public ActionResult Save(CostCenter costCenter)
        {
            ModelState.Remove("Id");

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            if (costCenter.Id == 0)
                _costCenterRepository.AddToDatabase(costCenter);
            else
            {
                var costCenterInDB = _costCenterRepository.FindById(costCenter.Id);

                costCenterInDB.Name = costCenter.Name;
                costCenterInDB.Description = costCenter.Description;
            }

            _costCenterRepository.Save();

            return RedirectToAction("Index");
        }

        // GET: CostCenter/Edit
        public ActionResult Edit(int id)
        {
            var costCenter = _costCenterRepository.FindById(id);

            if (costCenter == null)
                return HttpNotFound();

            return PartialView("_CostCenterForm", costCenter);
        }

        // CostCenter/Delete/Id
        public JsonResult Delete(int id)
        {
            var costCenterInDB = _costCenterRepository.FindById(id);
            var users = _applicationUserRepository.GetAllFromDatabaseEnumerable().Where(u => u.CostCenterId == id).ToList();
            bool result = false;

            if (users.Count == 0 && costCenterInDB != null)
            {
                _costCenterRepository.DeleteFromDatabase(costCenterInDB);
                _costCenterRepository.Save();
                result = true;
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}