using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Models.Shopping;

namespace WebApp.Controllers
{
    //[AuthorizeUser]
    public class ShoppingCartController : Controller
    {
        public CartHandler CartHandler { get; set; }
        public OrderHandler OrderHandler { get; set; }

        public ActionResult Index()
        {
            var model = GetCartModel();
            return View(model);
        }

        public ActionResult Add(int productId = 0, int productCount = 0)
        {
            var cart = GetCart();
            CartHandler.AddProduct(productId, productCount, cart);
            var member = PublicUser.GetCurrentUser();
            if (member == null || member.MemberId <= 0)
            {
                Session[Constants.CartKeyName] = cart;
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeProductCount(int productId = 0, int productCount = 0)
        {
            var cart = GetCart();
            CartHandler.ChangeProductCount(productId, productCount, cart);
            var member = PublicUser.GetCurrentUser();
            if (member == null || member.MemberId <= 0)
            {
                Session[Constants.CartKeyName] = cart;
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveProduct(int productId = 0)
        {
            var cart = GetCart();
            CartHandler.ChangeProductCount(productId, 0, cart);
            var member = PublicUser.GetCurrentUser();
            if (member == null || member.MemberId <= 0)
            {
                Session[Constants.CartKeyName] = cart;
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MiniShoppingCart()
        {
            var model = GetCartModel();
            return PartialView("MiniShoppingCart", model);
        }

        public ActionResult RefreshShoppingCart()
        {
            var model = GetCartModel();
            return PartialView("CartItemsPartial", model);
        }

        public ActionResult Confirm()
        {
            var cart = GetCart();
            OrderHandler.ConfirmOrder(ref cart);
            CartHandler.SaveCart(cart);

            return View("Confirm");
        }

        private Cart GetCart()
        {
            var member = PublicUser.GetCurrentUser();
            if (member != null && member.MemberId > 0)
            {
                return CartHandler.GetCart(member.MemberId);
            }
            var cart = (Cart)Session[Constants.CartKeyName];
            if (cart == null)
            {
                cart = new Cart();
                Session[Constants.CartKeyName] = cart;
            }
            return cart;
        }

        private CartModel GetCartModel()
        {
            var cart = GetCart();
            var model = CartModelMapper.Map(cart);
            return model;
        }
    }
}