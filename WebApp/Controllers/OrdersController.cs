using System.Linq;
using System.Web.Mvc;
using BusinessLayer;
using RepositoryLayer;
using WebApp.Models.Shopping;

namespace WebApp.Controllers
{
    [AuthorizeUser]
    public class OrdersController : Controller
    {
        public ActionResult Index()
        {
            var member = PublicUser.GetCurrentUser();
            var orderHandler = new OrderHandler(new OrdersRepository());
            var list = orderHandler.GetOrders(member.MemberId);
            var orderModels = list.Select(OrderModelMapper.Map).ToList();
            return View(orderModels);
        }

        public ActionResult OrderDetails(int id=0)
        {
            if (id == 0)
            {
                RedirectToAction("Index");
            }
     
            var orderHandler = new OrderHandler(new OrdersRepository());
            var order = orderHandler.GetOrder(id);
            var orderModel = OrderModelMapper.Map(order);
            return View(orderModel);
        }
    }
}