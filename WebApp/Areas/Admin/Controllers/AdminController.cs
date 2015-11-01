using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Models;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var user = (AdminUser)Session[Constants.AdminUserKeyName];
            if (user == null)
            {
                return RedirectToAction("Login", "Admin", new { area = "Admin" });
            }

            var admin = AdminHandler.GetUser(user.UserId);
            var userModel = AdminModelMapper.Map(admin);
            return View("Index",userModel);
        }

        [AuthorizeAdminAttribute]
        public ViewResult Edit()
        {
            var user = (AdminUser)Session[Constants.AdminUserKeyName];
            var profile = AdminHandler.GetUser(user.UserId);
            var profileModel = AdminModelMapper.Map(profile);
            return View(profileModel);
        }

        [HttpPost]
        [AuthorizeAdminAttribute]
        public ActionResult Edit(AdminModel model)
        {
            var user = AdminModelMapper.Map(model);
            AdminHandler.Update(user);
            return RedirectToAction("Index");
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