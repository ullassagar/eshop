using System;
using System.Collections.Generic;
using DataLayer;

namespace WebApp.Models.Shopping
{
    public class OrderModel : MasterModel
    {
        public int OrderId { get; set; }
        public decimal OrderValue { get; set; }
        public OrderStatusType CurrentOrderStatus { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCbm { get; set; }

        public OrderModel()
        {
            OrderItems = new List<OrderItemModel>();
        }
    }

    public class OrderModelMapper
    {
        public static OrderModel Map(Order order)
        {
            var model = new OrderModel();
            if (order != null)
            {
                model.OrderId = order.OrderId;
                model.OrderValue = order.OrderValue;
                model.CurrentOrderStatus = order.CurrentOrderStatus;
                model.CreationDate = order.CreationDate;
                model.TotalCbm = order.TotalCbm;
                model.OrderItems = new List<OrderItemModel>();
                if (order.OrderItems != null)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        model.OrderItems.Add(OrderItemModelMapper.Map(orderItem));
                    }
                }
            }
            return model;
        }
    }
}