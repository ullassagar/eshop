using System.Linq;
using System.Web.Mvc;
using BusinessLayer;
using WebApp.Models.Shopping;

namespace WebApp.Controllers
{
    [AuthorizeUser]
    public class OrdersController : Controller
    {
        public OrderHandler OrderHandler { get; set; }

        public ActionResult Index()
        {
            var member = PublicUser.GetCurrentUser();
            var list = OrderHandler.GetOrders(member.MemberId);
            var orderModels = list.Select(OrderModelMapper.Map).ToList();
            return View(orderModels);
        }

        public ActionResult OrderDetails(int id = 0)
        {
            if (id == 0)
            {
                RedirectToAction("Index");
            }

            var order = OrderHandler.GetOrder(id);
            var orderModel = OrderModelMapper.Map(order);
            return View(orderModel);
        }
    }
}