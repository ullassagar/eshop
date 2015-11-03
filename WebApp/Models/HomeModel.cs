using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace WebApp.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            ProductList = new List<ProductModel>();
        }

        public List<ProductModel> ProductList { get; set; }
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string EanCode { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Cbm { get; set; }

        [Range(1, 20)]
        public int ProductCount { get; set; }
        public string Description { get; set; }

        public ProductModel()
        {
            ProductCount = 1;
        }
    }

    public class ProductModelMapper
    {
        public static ProductModel MapToProductModel(Product product)
        {
            var model = new ProductModel();
            if (product != null)
            {
                model.ProductId = product.ProductId;
                model.ProductName = product.ProductName;
                model.EanCode = product.EanCode;
                model.ImageUrl = product.ImageUrl;
                model.Price = product.Price;
                model.Length = product.Length;
                model.Width = product.Width;
                model.Height = product.Height;
                model.Cbm = product.Cbm;
                model.Description = product.Description;
            }
            return model;
        }
    }
}