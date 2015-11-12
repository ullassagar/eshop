using System.Collections.Generic;
using DataLayer;

namespace WebApp.Models.Shopping
{
    public class OrderModel : MasterModel
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }

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
                model.MemberId = order.MemberId;

                if (order.OrderItems != null)
                {
                    foreach (OrderItem orderItem in order.OrderItems)
                    {
                        model.OrderItems.Add(OrderItemModelMapper.Map(orderItem));
                    }
                }
            }
            return model;
        }
    }
}