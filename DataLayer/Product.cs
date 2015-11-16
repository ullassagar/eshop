using System;
using System.Data;
using UtilityLayer;

namespace DataLayer
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string EanCode { get; set; }
        public string ImageUrl { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOutOfStock { get; set; }
        public decimal Cbm
        {
            get { return Math.Round(((decimal)(Length * Width * Height) / 1000), 2); }
        }

        public static Product Load(IDataReader reader)
        {
            var product = new Product
            {
                ProductId = DbHelper.ConvertToInt32(reader["ProductId"]),
                ProductName = DbHelper.ConvertToString(reader["ProductName"]),
                EanCode = DbHelper.ConvertToString(reader["EanCode"]),
                ImageUrl = DbHelper.ConvertToString(reader["ImageUrl"]),
                Length = DbHelper.ConvertToInt32(reader["Length"]),
                Width = DbHelper.ConvertToInt32(reader["Width"]),
                Height = DbHelper.ConvertToInt32(reader["Height"]),
                Description = DbHelper.ConvertToString(reader["Description"]), 
                Price = DbHelper.ConvertToDecimal(reader["Price"]),
                IsOutOfStock = DbHelper.ConvertToBool(reader["IsOutOfStock"])
            };
            return product;
        }
    }
}
