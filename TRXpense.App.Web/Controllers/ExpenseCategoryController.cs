using PagedList;
using System.Linq;
using System.Web.Mvc;
using TRXpense.App.Web.Mappers;
using TRXpense.App.Web.ViewModels;
using TRXpense.Dal.Repositories;

namespace TRXpense.App.Web.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        public ExpenseCategoryController()
        {
            _expenseCategoryRepository = new ExpenseCategoryRepository();
        }

        // GET: ExpenseCategory
        public ActionResult Index(int? page, string query = null)
        {
            var expenseCategories = _expenseCategoryRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews();

            // paging
            var pageSize = 5;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfExpenseCategories = expenseCategories.ToPagedList(pageNumber, pageSize); // will only contain 5 products max because of the pageSize

            //searching
            if (!string.IsNullOrEmpty(query))
            {
                int i;
                var expenseCategorySearched = _expenseCategoryRepository.GetAllFromDatabaseEnumerable()
                    .Where(e => e.Name.ToLower().Contains(query.ToLower()) || e.Account.Equals(int.TryParse(query, out i) ? i : (int?)null))
                    .ToList()
                    .MapToViews();

                onePageOfExpenseCategories = expenseCategorySearched.ToPagedList(pageNumber, pageSize);
            }

            ViewBag.onePageOfExpenseCategories = onePageOfExpenseCategories;
            return View(onePageOfExpenseCategories);
        }

        public ActionResult Create()
        {
            return PartialView("_ExpenseCategoryForm");
        }

        public ActionResult Edit(int id)
        {
            var expenseCategory = _expenseCategoryRepository.FindById(id).MapToView();

            if (expenseCategory == null)
                return HttpNotFound();

            return PartialView("_ExpenseCategoryForm", expenseCategory);
        }

        [HttpPost]
        public ActionResult Save(ExpenseCategoryVM expenseCategory)
        {
            ModelState.Remove("id");

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            if (expenseCategory.Id == 0)
                _expenseCategoryRepository.AddToDatabase(expenseCategory.MapToModel());
            else
                _expenseCategoryRepository.UpdateInDatabase(expenseCategory.MapToModel(), expenseCategory.Id);

            _expenseCategoryRepository.Save();

            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var expenseCategoryInDb = _expenseCategoryRepository.FindById(id);
            bool result = false;

            if (expenseCategoryInDb != null)
            {
                _expenseCategoryRepository.DeleteFromDatabase(expenseCategoryInDb);
                _expenseCategoryRepository.Save();
                result = true;
            }
            else
                return Json(new { });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}