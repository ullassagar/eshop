using System.Web.Mvc;
using BusinessLayer;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[AuthorizeUser]
    public class HomeController : Controller
    {
        public ProductHandler ProductHandler { get; set; }

        public ActionResult Index()
        {
            var model = new HomeModel();
            var products = ProductHandler.GetList();

            foreach(var product in products)
            {
                var productModel = ProductModelMapper.MapToProductModel(product);
                model.ProductList.Add(productModel);
            }

            return View(model);
        }
    }
}