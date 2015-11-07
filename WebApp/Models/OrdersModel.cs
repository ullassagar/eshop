using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class OrdersModel
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public List<OrderDetailModel> OrderItems { get; set; }


        public OrdersModel()
        {
            OrderItems = new List<OrderDetailModel>();
        }
    }

    public class OrdersModelMapper
    {
        public static OrdersModel Map(Order order)
        {
            var model = new OrdersModel();
            if (order != null)
            {
                model.OrderId = order.OrderId;
                model.MemberId = order.MemberId;

                if (order.OrderItems != null)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        model.OrderItems.Add(OrderDetailModelMapper.Map(orderItem));
                    }
                }
            }
            return model;
        }
    }
}