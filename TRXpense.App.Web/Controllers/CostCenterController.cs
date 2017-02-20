using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.ViewModels;
using TRXpense.Bll.Model;
using TRXpense.Dal.Database;
using TRXpense.Dal.Repositories;
using System.Data.Entity;

namespace TRXpense.App.Web.Controllers
{
    public class CostCenterController : Controller
    {
        private readonly ICostCenterRepository _costCenterRepository;
        private readonly ApplicationDbContext _context;

        public CostCenterController()
        {
            _costCenterRepository = new CostCenterRepository();
            _context = new ApplicationDbContext();
        }

        // GET: CostCenter
        public ActionResult Index()
        {
            //var costCenters = _costCenterRepository.GetAllFromDatabaseEnumerable().ToList();
            //var costCenters = _context.Users.Select(c => c.CostCenter).ToList();

            return View(/*costCenters*/);
        }

        // GET: /CostCenter/Create
        public ActionResult CreateCostCenter()
        {
            return PartialView("_CostCenterForm");
        }

        // POST: /CostCenter/Create
        [HttpPost]
        public ActionResult Create(CostCenter costCenter)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RegisterViewModel
                {
                    ApplicationUser = new ApplicationUser(),
                    CostCenters = _costCenterRepository.GetAllFromDatabaseEnumerable().ToList()
                };

                return View("Register", viewModel);
            }

            if (costCenter.Id == 0)
            {
                _costCenterRepository.AddToDatabase(costCenter);
                _costCenterRepository.Save();
            }

            //if (customer.Id == 0)
            //{
            //    _context.Customers.Add(customer);
            //}
            //else
            //{
            //    var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

            //    //TryUpdateModel(customerInDb);

            //    customerInDb.Name = customer.Name;
            //    customerInDb.Birthdate = customer.Birthdate;
            //    customerInDb.MembershipTypeId = customer.MembershipTypeId;
            //    customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //}

            //_context.SaveChanges();

            return RedirectToAction("Register", "Account");
        }
    }
}