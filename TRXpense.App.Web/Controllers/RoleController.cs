using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using TRXpense.Dal.Database;

namespace TRXpense.App.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(int? page, string query = null)
        {
            var roles = _context.Roles.ToList();

            if (TempData["message"] != null)
                ViewBag.Message = TempData["message"].ToString();

            // paging
            int pageSize = 5;
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfRoles = roles.ToPagedList(pageNumber, pageSize); // will only contain 5 products max because of the pageSize

            // searching
            if (!string.IsNullOrEmpty(query))
            {
                var roleSearched = _context.Roles.Where(r => r.Name.ToLower().Contains(query.ToLower())).ToList();

                onePageOfRoles = roleSearched.ToPagedList(pageNumber, pageSize);
            }

            ViewBag.onePageOfRoles = onePageOfRoles;
            return View(onePageOfRoles);
        }

        public ActionResult Details(string roleName)
        {
            var users = _context.Users.Where(u => u.UserRole == roleName).ToList();

            return PartialView("_Details", users);
        }

        // GET: /Roles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            return PartialView("_RoleForm");
        }

        // POST: /Roles/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    _context.Roles.Add(new IdentityRole()
        //    {
        //        Name = collection["RoleName"]
        //    });
        //    _context.SaveChanges();
        //    ViewBag.ResultMessage = "Role created successfully !";
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Save(IdentityRole role)
        {
            if (role.Name == null)
                return RedirectToAction("Index");

            if (role.Id == null)
                _context.Roles.Add(new IdentityRole()
                {
                    Name = role.Name
                });
            else
            {
                var roleInDb = _context.Roles.Find(role.Id);
                roleInDb.Name = role.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: /Roles/Edit/5
        //public ActionResult Edit(string roleName)
        //{
        //    var thisRole = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        //    return View(thisRole);
        //}

        public ActionResult Edit(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return PartialView("_RoleForm", role);
        }

        // POST: /Roles/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(IdentityRole role)
        //{
        //    _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return PartialView("_Delete", role);
        }

        //public ActionResult Delete(string roleName)
        //{
        //    var thisRole = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //    _context.Roles.Remove(thisRole);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            var roleInDb = _context.Roles.Find(role.Id);

            if (roleInDb.Users.Count == 0 && roleInDb != null)
            {
                _context.Roles.Remove(roleInDb);
                _context.SaveChanges();
            }
            else
            {
                TempData["message"] = "There are employees assigned to this Role! You can only delete roles with no employees.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}