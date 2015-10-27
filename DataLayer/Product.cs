using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLayer;

namespace DataLayer
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string EanCode { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Cbm
        {
            get { return Math.Round(((decimal)(Length * Width * Height) / 1000), 2); }
        }

        public static Product Load(IDataReader reader)
        {
            var product = new Product();
            product.ProductId = DbHelper.ConvertToInt32(reader["ProductId"]);
            product.ProductName = DbHelper.ConvertToString(reader["ProductName"]);
            product.EanCode = DbHelper.ConvertToString(reader["EanCode"]);
            product.ImageUrl = DbHelper.ConvertToString(reader["ImageUrl"]);
            product.Price = DbHelper.ConvertToDecimal(reader["Price"]);
            product.Length = DbHelper.ConvertToInt32(reader["Length"]);
            product.Width = DbHelper.ConvertToInt32(reader["Width"]);
            product.Height = DbHelper.ConvertToInt32(reader["Height"]);
            return product;
        }
    }
}
