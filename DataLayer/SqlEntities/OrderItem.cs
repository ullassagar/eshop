using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using UtilityLayer;

namespace DataLayer
{
    public class OrderItem
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPriceOut { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    
        public decimal Cbm
        {
            get { return Math.Round(((decimal)(Length * Width * Height) / 1000), 2); }
        }
    
        public decimal TotalCbm
        {
            get { return Math.Round(ProductCount * Cbm, 2); }
        }

        public static OrderItem Load(IDataReader reader)
        {
            return new OrderItem
            {
                OrderDetailId = DbHelper.ConvertToInt32(reader["OrderDetailId"]),
                OrderId = DbHelper.ConvertToInt32(reader["OrderId"]),
                ProductId = DbHelper.ConvertToInt32(reader["ProductId"]),
                ProductCount = DbHelper.ConvertToInt32(reader["ProductCount"]),
                PriceOut = DbHelper.ConvertToDecimal(reader["PriceOut"]),
                TotalPriceOut = DbHelper.ConvertToDecimal(reader["TotalPriceOut"]),
                ProductName = DbHelper.ConvertToString(reader["ProductName"]),
                ImageUrl = DbHelper.ConvertToString(reader["ImageUrl"]),
                Length = DbHelper.ConvertToInt32(reader["Length"]),
                Width = DbHelper.ConvertToInt32(reader["Width"]),
                Height = DbHelper.ConvertToInt32(reader["Height"])
            };
        }
    }
}
