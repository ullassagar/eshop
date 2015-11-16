using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Models.Customers;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public MemberHandler MemberHandler { get; set; }
        public CartHandler CartHandler { get; set; }

        [AuthorizeUser]
        public ActionResult Index()
        {
            var user = (PublicUser) Session[Constants.AppUserKeyName];
            var member = MemberHandler.GetMember(user.MemberId);
            var memberModel = MemberModeMapper.Map(member);
            return View(memberModel);
        }

        [AuthorizeUser]
        public ViewResult Edit()
        {
            var user = (PublicUser) Session[Constants.AppUserKeyName];
            var member = MemberHandler.GetMember(user.MemberId);
            var memberModel = MemberModeMapper.Map(member);
            return View(memberModel);
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult Edit(MemberModel model)
        {
            var member = MemberModeMapper.Map(model);
            MemberHandler.Update(member);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberModel model)
        {
            var member = MemberHandler.GetMember(model.EmailAddress, model.Password);
            if (member != null)
            {
                Session[Constants.AppUserKeyName] = PublicUser.GetCurrentUser(member);

                var mongoCart = CartHandler.GetCart(member.MemberId);
                var sessionCart = (Cart) Session[Constants.CartKeyName];

                if (sessionCart != null && sessionCart.Items != null && sessionCart.Items.Count > 0)
                {
                    if (mongoCart != null && mongoCart.Items != null && mongoCart.Items.Count > 0)
                    {
                        foreach (var sessionItem in sessionCart.Items)
                        {
                            var mongoItem = mongoCart.Items.Find(p => p.ProductId == sessionItem.ProductId);
                            if (mongoItem != null)
                            {
                                mongoItem.ProductCount = sessionItem.ProductCount;
                            }
                            else
                            {
                                mongoCart.Items.Add(sessionItem);
                            }
                        }
                    }
                    else
                    {
                        mongoCart = sessionCart;
                    }
                }

                Session[Constants.CartKeyName] = null;
                CartHandler.SaveCart(mongoCart);

                return RedirectToAction("Index", "Home", new {area = ""});
            }

            ViewBag.LoginError = "Username and password does not match.";
            return View();
        }

        [HttpPost]
        public ActionResult Register(MemberModel model)
        {
            var member = MemberModeMapper.Map(model);
            var error = MemberHandler.AddMember(member);

            if (error == ErrorCode.ErrorWhileMemberRegistrationEmailEmpty)
            {
                ViewBag.RegstrationError = "Email address can't be empty.";
                return View("Login");
            }

            if (error == ErrorCode.ErrorWhileMemberRegistrationPasswordEmpty)
            {
                ViewBag.RegstrationError = "Password can't be empty.";
                return View("Login");
            }

            if (error == ErrorCode.ErrorWhileMemberRegistrationEmailAlreadyExist)
            {
                ViewBag.RegstrationError = "Email address already registerd.";
                return View("Login");
            }

            Session[Constants.AppUserKeyName] = PublicUser.GetCurrentUser(member);
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        [AuthorizeUser]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session[Constants.AppUserKeyName] = null;
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}