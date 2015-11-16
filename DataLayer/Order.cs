using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UtilityLayer;

namespace DataLayer
{
    public enum OrderStatus
    {
        UnKnown = 0,
        Confirmed = 1,
        BeingPicked = 2,
        Picked = 3,
        Delivered = 4,
        Cancelled = 5
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public decimal OrderValue { get; set; }
        public OrderStatus CurrentOrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }
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

        public static Order Load(IDataReader reader)
        {
            var order = new Order();
            order.OrderId = DbHelper.ConvertToInt32(reader["OrderId"]);
            order.MemberId = DbHelper.ConvertToInt32(reader["MemberId"]);
            order.CurrentOrderStatus = (OrderStatus)DbHelper.ConvertToInt32(reader["OrderStatusId"]);
            order.OrderValue = DbHelper.ConvertToDecimal(reader["OrderValue"]);
            order.CreationDate = DbHelper.ConvertToDateTime(reader["CreationDate"]);
            return order;
        }
    }
}
