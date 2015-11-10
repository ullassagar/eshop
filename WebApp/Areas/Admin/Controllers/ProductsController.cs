using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        public ProductHandler ProductHandler { get; set; }

        public ActionResult Index()
        {
            var model = new ProductsModel();
            List<Product> proList = ProductHandler.GetList();
            foreach (Product pro in proList)
            {
                ProductsModel proModel = ProductModelMapper.MapToProductModel(pro);
                model.ProductList.Add(proModel);
            }
            return View(model);
        }

        public ActionResult Add()
        {
            var u = new ProductsModel();
            return View(u);
        }

        [HttpPost]
        public ActionResult Add(ProductsModel model, HttpPostedFileBase file)
        {
            Product product = ProductModelMapper.MapToProduct(model);
            try
            {
                ProductHandler.Add(product);
                product.ImageUrl = product.ProductId + ".png";
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Images/Products"), product.ImageUrl);
                    file.SaveAs(path);
                }
                ProductHandler.Update(product);
            }
            catch (Exception ex)
            {
                model.Error = ex.Message;
                return View("Add", model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            Product product = ProductHandler.GetProduct(id);
            ProductsModel proModel = ProductModelMapper.MapToProductModel(product);
            return View(proModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductsModel model, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Images/Products"), model.ImageUrl);
                    file.SaveAs(path);
                }
                Product product = ProductModelMapper.MapToProduct(model);
                ProductHandler.Update(product);
            }
            catch (Exception ex)
            {
                model.Error = ex.Message;
                return View("Edit", model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            ProductHandler.Delete(id);
            return RedirectToAction("Index");
        }
    }
}