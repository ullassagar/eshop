using BusinessLayer;
using DataLayer;
using WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var product = ProductHandler.GetProduct(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var promodel = ProductModelMapper.MapToProductModel(product);
            return View(promodel);
        }
    }
}