using System.Web.Mvc;

namespace WebApp.Controllers
{
    [AuthorizeUser]
    public class OrdersController : Controller
    {
        //
        // GET: /Orders/
        public ActionResult Index()
        {
            return View();
        }
    }
}