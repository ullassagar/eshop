﻿using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Models;
using WebApp.Models.Products;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        public ProductHandler ProductHandler { get; set; }

        public ActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            Product product = ProductHandler.GetProduct(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ProductModel promodel = ProductModelMapper.Map(product);

            return View(promodel);
        }
    }
}