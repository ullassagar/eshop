using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        public ProductHandler ProductHandler { get; set; }

        public ActionResult Index()
        {
            var model = new ProductModel();
            var proList = ProductHandler.GetList();
            foreach (var pro in proList)
            {
                var proModel = ProductModelMapper.Map(pro);
                model.ProductList.Add(proModel);
            }
            return View(model);
        }

        public ActionResult Add()
        {
            var u = new ProductModel();
            return View(u);
        }

        [HttpPost]
        public ActionResult Add(ProductModel model, HttpPostedFileBase file)
        {
            var product = ProductModelMapper.Map(model);
            try
            {
                product.CreationDate = DateTime.Now;
                ProductHandler.Add(product);
                product.ImageUrl = product.ProductId + ".png";
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Products"), product.ImageUrl);
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
            var product = ProductHandler.GetById(id);
            var proModel = ProductModelMapper.Map(product);
            return View(proModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductModel model, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Products"), model.ImageUrl);
                    file.SaveAs(path);
                }
                var product = ProductModelMapper.Map(model);
                product.LastModifiedDate = DateTime.Now;
                ProductHandler.Update(product);
            }
            catch (Exception ex)
            {
                model.Error = ex.Message;
                return View("Edit", model);
            }
            return RedirectToAction("Index");
        }
    }
}