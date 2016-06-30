using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using UtilityLayer;

namespace DataLayer
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public decimal OrderValue { get; set; }
        public int LatestOrderStatusId { get; set; }
        public OrderStatusType CurrentOrderStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCbm
        {
            get
            {
                decimal total = 0;
                if (OrderItems != null)
                {
                    total += OrderItems.Sum(item => item.TotalCbm);
                }
                return Math.Round(total, 2);
            }
        }
    }
}
