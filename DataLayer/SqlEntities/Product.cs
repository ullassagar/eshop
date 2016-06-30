using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product
    {
        [Key]
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
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public Category Category { get; set; }

        public decimal Cbm
        {
            get { return Math.Round(((decimal) (Length*Width*Height)/1000), 2); }
        }
    }
}