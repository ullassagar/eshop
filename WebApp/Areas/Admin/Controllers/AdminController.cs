using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var user = (AdminUser) Session[Constants.AdminUserKeyName];
            if (user == null)
            {
                return RedirectToAction("Login", "Admin", new {area = "Admin"});
            }
            return RedirectToAction("Index", "Products", new {area = "Admin"});
        }

        [AuthorizeAdmin]
        public ActionResult ViewProfile()
        {
            var user = (AdminUser) Session[Constants.AdminUserKeyName];
            if (user == null)
            {
                return RedirectToAction("Login", "Admin", new {area = "Admin"});
            }

            var adminHandler = new AdminHandler();
            User admin = adminHandler.GetUser(user.UserId);
            AdminModel userModel = AdminModelMapper.Map(admin);
            return View("ViewProfile", userModel);
        }

        [AuthorizeAdmin]
        public ViewResult Edit()
        {
            var user = (AdminUser) Session[Constants.AdminUserKeyName];
            var adminHandler = new AdminHandler();
            User profile = adminHandler.GetUser(user.UserId);
            AdminModel profileModel = AdminModelMapper.Map(profile);
            return View(profileModel);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public ActionResult Edit(AdminModel model)
        {
            User user = AdminModelMapper.Map(model);
            var adminHandler = new AdminHandler();
            adminHandler.Update(user);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminModel model)
        {
            var adminHandler = new AdminHandler();
            User user = adminHandler.GetUser(model.EmailId, model.Password);
            if (user != null)
            {
                Session[Constants.AdminUserKeyName] = AdminUser.GetCurrentUser(user);
                return RedirectToAction("Index", "Products", new {area = "Admin"});
            }
            ViewBag.Message = "No Access!! Invalid User for this module";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session[Constants.AdminUserKeyName] = null;
            return RedirectToAction("Login", "Admin", new {area = "Admin"});
        }
    }
}