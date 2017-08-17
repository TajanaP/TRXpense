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
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseCategoryController()
        {
            _expenseCategoryRepository = new ExpenseCategoryRepository();
            _expenseRepository = new ExpenseRepository();
        }

        // GET: ExpenseCategory
        public ActionResult Index(int? page, string query = null)
        {
            var expenseCategories = _expenseCategoryRepository.GetAllFromDatabaseEnumerable().ToList().MapToViews().OrderBy(o => o.Name);

            // paging
            var pageSize = 5;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfExpenseCategories = expenseCategories.ToPagedList(pageNumber, pageSize); // will only contain 5 items max because of the pageSize

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
        public ActionResult Save(ExpenseCategoryVM view)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (view.Id == 0)
                    _expenseCategoryRepository.AddToDatabase(view.MapToModel());
                else
                    _expenseCategoryRepository.UpdateInDatabase(view.MapToModel(), view.Id);

                _expenseCategoryRepository.Save();
            }
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var expenseCategoryInDb = _expenseCategoryRepository.FindById(id);
            bool result = false;
            var expensesThatHaveThisCategory =
                _expenseRepository.GetAllFromDatabaseEnumerable().Where(e => e.ExpenseCategoryId == id).ToList();

            if (expensesThatHaveThisCategory.Count == 0 && expenseCategoryInDb != null)
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