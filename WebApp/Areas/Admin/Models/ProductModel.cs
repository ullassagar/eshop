using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace WebApp.Areas.Admin.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Product name required")]
        public string ProductName { get; set; }
        public string EanCode { get; set; }
        public string ImageUrl { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOutOfStock { get; set; }
        public string Error { get; set; }
        public List<ProductModel> ProductList { get; set; }
        public ProductModel()
        {
            ProductList = new List<ProductModel>();
        }

        public System.DateTime CreationDate { get; set; }

        public System.DateTime? LastModifiedDate { get; set; }
    }

    public class ProductModelMapper
    {
        public static ProductModel Map(Product product)
        {
            var model = new ProductModel();
            if (product != null)
            {
                model.ProductId = product.ProductId;
                model.ProductName = product.ProductName;
                model.EanCode = product.EanCode;
                model.ImageUrl = product.ImageUrl;
                model.Length = product.Length;
                model.Width = product.Width;
                model.Height = product.Height;
                model.Description = product.Description;
                model.Price = product.Price;
                model.IsOutOfStock = product.IsOutOfStock;
                model.CreationDate = product.CreationDate;
                model.LastModifiedDate = product.LastModifiedDate;
            }
            return model;
        }

        public static Product Map(ProductModel model)
        {
            var product = new Product();
            if (model != null)
            {
                product.ProductId = model.ProductId;
                product.ProductName = model.ProductName;
                product.EanCode = model.EanCode;
                product.ImageUrl = model.ImageUrl;
                product.Length = model.Length;
                product.Width = model.Width;
                product.Height = model.Height;
                product.Description = model.Description;
                product.Price = model.Price;
                product.IsOutOfStock = model.IsOutOfStock;
                product.CreationDate = model.CreationDate;
                product.LastModifiedDate = model.LastModifiedDate;
            }
            return product;
        }
    }
}