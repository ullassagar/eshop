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
            Member member = MemberHandler.GetMember(user.MemberId);
            MemberModel memberModel = MemberModeMapper.Map(member);
            return View(memberModel);
        }

        [AuthorizeUser]
        public ViewResult Edit()
        {
            var user = (PublicUser) Session[Constants.AppUserKeyName];
            Member member = MemberHandler.GetMember(user.MemberId);
            MemberModel memberModel = MemberModeMapper.Map(member);
            return View(memberModel);
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult Edit(MemberModel model)
        {
            Member member = MemberModeMapper.Map(model);
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
            Member member = MemberHandler.GetMember(model.EmailAddress, model.Password);
            if (member != null)
            {
                Session[Constants.AppUserKeyName] = PublicUser.GetCurrentUser(member);

                Cart mongoCart = CartHandler.GetCart(member.MemberId);
                var sessionCart = (Cart) Session[Constants.CartKeyName];

                if (sessionCart != null && sessionCart.Items != null && sessionCart.Items.Count > 0)
                {
                    if (mongoCart != null && mongoCart.Items != null && mongoCart.Items.Count > 0)
                    {
                        foreach (CartItem sessionItem in sessionCart.Items)
                        {
                            CartItem mongoItem = mongoCart.Items.Find(p => p.ProductId == sessionItem.ProductId);
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
            Member member = MemberModeMapper.Map(model);
            ErrorCode error = MemberHandler.AddMember(member);

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