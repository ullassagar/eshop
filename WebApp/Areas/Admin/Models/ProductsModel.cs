using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace WebApp.Areas.Admin.Models
{
    public class ProductsModel
    {
        public int ProductId { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Product name required")]
        public string ProductName { get; set; }
        public string EanCode { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Cbm
        {
            get { return Length * Width * Height; }
        }
        public string Description { get; set; }

        public string Error { get; set; }

        public List<ProductsModel> ProductList { get; set; }
        public ProductsModel()
        {
            ProductList = new List<ProductsModel>();
        }
    }

    public class ProductModelMapper
    {
        public static ProductsModel MapToProductModel(Product product)
        {
            var model = new ProductsModel();
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
                model.Description = product.Description;
            }
            return model;
        }

        public static Product MapToProduct(ProductsModel model)
        {
            var product = new Product();
            if (model != null)
            {
                product.ProductId = model.ProductId;
                product.ProductName = model.ProductName;
                product.EanCode = model.EanCode;
                product.ImageUrl = model.ImageUrl;
                product.Price = model.Price;
                product.Length = model.Length;
                product.Width = model.Width;
                product.Height = model.Height;
                product.Description = model.Description;
            }
            return product;
        }
    }
}