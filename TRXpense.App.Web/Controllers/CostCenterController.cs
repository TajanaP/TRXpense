using PagedList;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.Mappers;
using TRXpense.App.Web.ViewModels;
using TRXpense.Dal.Repositories;

namespace TRXpense.App.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult Index(int? page, string query = null)
        {
            var costCenters = _costCenterRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews().OrderBy(o => o.Name);

            // paging
            int pageSize = 5;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfCostCenters = costCenters.ToPagedList(pageNumber, pageSize); // will only contain 5 items max because of the pageSize

            // searching
            if (!string.IsNullOrEmpty(query))
            {
                var costCenterSearched = _costCenterRepository.GetAllFromDatabaseEnumerable()
                    .Where(c => c.Name.ToLower().Contains(query.ToLower()) || c.Description.ToLower().Contains(query.ToLower()))
                    .ToList()
                    .MapToViews();

                onePageOfCostCenters = costCenterSearched.ToPagedList(pageNumber, pageSize);
            }

            ViewBag.onePageOfCostCenters = onePageOfCostCenters;
            return View(onePageOfCostCenters);
        }

        // GET: CostCenter/Details/Id
        public ActionResult Details(int id)
        {
            var users = _applicationUserRepository.GetAllFromDatabaseEnumerable().Where(u => u.CostCenterId == id).ToList().MapToViews();

            return PartialView("_Details", users);
        }

        // GET: /CostCenter/Create
        public ActionResult Create()
        {
            return PartialView("_CostCenterForm");
        }

        // GET: CostCenter/Edit
        public ActionResult Edit(int id)
        {
            var costCenter = _costCenterRepository.FindById(id).MapToView();

            if (costCenter == null)
                return HttpNotFound();

            return PartialView("_CostCenterForm", costCenter);
        }

        // POST: /CostCenter/Create
        [HttpPost]
        public ActionResult Save(CostCenterVM view)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (view.Id == 0)
                    _costCenterRepository.AddToDatabase(view.MapToModel());
                else
                    _costCenterRepository.UpdateInDatabase(view.MapToModel(), view.Id);

                _costCenterRepository.Save();
            }
            return RedirectToAction("Index");
        }

        // CostCenter/Delete/Id
        public JsonResult Delete(int id)
        {
            var costCenterInDB = _costCenterRepository.FindById(id);
            var users = _applicationUserRepository.GetAllFromDatabaseEnumerable().Where(u => u.CostCenterId == id).ToList().MapToViews();
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