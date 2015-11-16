using System.Web.Mvc;
using BusinessLayer;
using WebApp.Models;
using WebApp.Models.Products;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ProductHandler ProductHandler { get; set; }

        public ActionResult Index()
        {
            var model = new HomeModel();
            var products = ProductHandler.GetList(false);

            foreach(var product in products)
            {
                var productModel = ProductModelMapper.Map(product);
                model.Products.Add(productModel);
            }

            return View(model);
        }
    }
}