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
        public DateTime CreationDate { get; set; }

        public decimal Cbm
        {
            get { return Math.Round(((decimal)(Length * Width * Height) / 1000), 2); }
        }
    
        public decimal TotalCbm
        {
            get { return Math.Round(ProductCount * Cbm, 2); }
        }
    }
}
