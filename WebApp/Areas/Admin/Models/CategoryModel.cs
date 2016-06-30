using System;
using AutoMapper;
using DataLayer;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category name required")]
        public string CategoryName { get; set; }
        public DataLayer.Category ParentCategory { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        static CategoryModel()
        {
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<CategoryModel, Category>();
        }

        public string Error { get; set; }
    }
}