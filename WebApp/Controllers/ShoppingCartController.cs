using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[AuthorizeUser]
    public class ShoppingCartController : Controller
    {
        //
        // GET: /CartItems/
        public ActionResult Index()
        {
            var model = GetCartModel();
            return View(model);
        }

        public ActionResult Add(int productId = 0, int productCount = 0)
        {
            var cart = GetCart();
            var cartHandler = new CartHandler();
            cartHandler.AddProduct(productId, productCount, cart);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeProductCount(int productId = 0, int productCount = 0)
        {
            var cart = GetCart();
            var cartHandler = new CartHandler(); 
            cartHandler.ChangeProductCount(productId, productCount, cart);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveProduct(int productId = 0)
        {
            var cart = GetCart();
            var cartHandler = new CartHandler(); 
            cartHandler.ChangeProductCount(productId, 0, cart);
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
            var orderHandler = new OrderHandler();
            orderHandler.AddOrder(cart);

            Session[Constants.CartKeyName] = new Cart();
            var cartHandler = new CartHandler(); 
            cartHandler.SaveCart(cart);

            return View("Confirm");
        }

        private Cart GetCart()
        {
            var member = PublicUser.GetCurrentUser();
            if (member != null && member.MemberId > 0)
            {
                var cartHandler = new CartHandler();
                return cartHandler.GetCart(member.MemberId);
            }
            else
            {
                var cart = (Cart)Session[Constants.CartKeyName];
                if (cart == null)
                {
                    cart = new Cart();
                    Session[Constants.CartKeyName] = cart;
                }
                return cart;
            }
        }

        private CartModel GetCartModel()
        {
            var cart = GetCart();
            var model = CartModelMapper.Map(cart);
            return model;
        }
    }
}