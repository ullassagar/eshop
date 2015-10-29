using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Models;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/
        public ActionResult Index()
        {
            var user = (AdminUser)Session[Constants.AdminUserKeyName];
            var admin = AdminHandler.GetUser(user.UserId);
            var userModel = AdminModelMapper.Map(admin);
            return View(userModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminModel model)
        {
            var user = AdminHandler.GetUser(model.EmailId, model.Password);
            if (user != null)
            {
                Session[Constants.AdminUserKeyName] = AdminUser.GetCurrentUser(user);
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            ViewBag.Message = "No Access!! Invalid User for this module";
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session[Constants.AdminUserKeyName] = null;
            return RedirectToAction("Login", "Admin", new { area = "Admin" });
        }
    }
}