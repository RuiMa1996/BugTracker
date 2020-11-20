using BugTracker.Models;
using System.Web.Mvc;

namespace TaskManagementProj.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            ViewBag.Users = UserManager.GetAllUserNames();
            return View();
        }

        public ActionResult AddUserToRole(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(string roleName, string userId)
        {
            var r = UserManager.AddUserToRole(userId, roleName);
            if (r.Succeeded)
            {
                db.SaveChanges();
            }

            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            db.Dispose();
            return RedirectToAction("index");
        }

        public ActionResult RemoveUserFromeRole(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserFromeRole(string roleName, string userId)
        {
            var r = UserManager.RemoveUserFromRole(userId, roleName);
            if (r.Succeeded)
            {
                db.SaveChanges();
            }
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            db.Dispose();
            return RedirectToAction("index");
        }
    }
}