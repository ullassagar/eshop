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
        public ActionResult Index()
        {
            var model = new ProductsModel();
            var proList = ProductHandler.GetList();

            foreach (var pro in proList)
            {
                var proModel = ProductModelMapper.MapToProductModel(pro);
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
            var product = ProductModelMapper.MapToProduct(model);

            try
            {
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
            var product = ProductHandler.GetProduct(id);

            var proModel = ProductModelMapper.MapToProductModel(product);

            return View(proModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductsModel model, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Images/Products"), model.ImageUrl);
                    file.SaveAs(path);
                }

                var product = ProductModelMapper.MapToProduct(model);
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