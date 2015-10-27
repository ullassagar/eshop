using System;
using System.Collections.Generic;
using System.Linq;
using MongoRepository;

namespace DataLayer
{
    public class Cart : Entity
    {
        public int MemberId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public int CartProductCount
        {
            get
            {
                int total = 0;
                if (CartItems != null)
                {
                    total += CartItems.Sum(cartItem => cartItem.ProductCount);
                }
                return total;
            }
        }

        public decimal CartTotalCbm
        {
            get
            {
                decimal total = 0;
                if (CartItems != null)
                {
                    total += CartItems.Sum(cartItem => cartItem.TotalCbm);
                }
                return Math.Round(total, 2);
            }
        }

        public decimal CartTotalPriceOut
        {
            get
            {
                decimal total = 0;
                if (CartItems != null)
                {
                    total += CartItems.Sum(cartItem => cartItem.TotalPriceOut);
                }
                return Math.Round(total, 2);
            }
        }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
