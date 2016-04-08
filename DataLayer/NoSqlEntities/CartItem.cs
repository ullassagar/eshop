using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal PriceOut { get; set; }
        public decimal Vat { get; set; }
        public decimal Cbm { get; set; }
        public int ProductCount { get; set; }

        public decimal TotalCbm
        {
            get { return Math.Round(ProductCount * Cbm, 2); }
        }

        public decimal TotalPriceOut
        {
            get { return Math.Round(ProductCount * PriceOut, 2); }
        }
    }
}
