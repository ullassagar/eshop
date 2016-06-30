using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using WebApp.Areas.Admin.Models;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryHandler Handler { get; set; }

        public ActionResult Index()
        {
            var modelList = new List<CategoryModel>();

            var entityList = Handler.GetAll();

            if (entityList != null)
            {
                foreach (var item in entityList)
                {
                    var model = item.ToViewModel<Category, CategoryModel>();
                    modelList.Add(model);
                }
            }

            return View(modelList);
        }

        public ActionResult Add()
        {
            return View(new CategoryModel());
        }

        [HttpPost]
        public ActionResult Add(CategoryModel model)
        {
            try
            {
                var entity = model.ToEntity<CategoryModel, Category>();
                entity.CreationDate = DateTime.Now;
                Handler.Add(entity);
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
            var entity = Handler.GetById(id);
            var model = entity.ToViewModel<Category, CategoryModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            try
            {
                var entity = model.ToEntity<CategoryModel, Category>();
                entity.LastModifiedDate = DateTime.Now;
                Handler.Update(entity);
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