using System;
using System.Collections.Generic;
using System.Data;
using DataLayer;
using RepositoryLayer.Infrastructure;
using System.Linq;

namespace RepositoryLayer
{
    public class OrdersRepository : GenericSqlRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<int> GetOrderIds(int memberId)
        {
            var qIds = from order in this.DataContext.Orders
                       where order.MemberId == memberId
                       select order.OrderId;

            return qIds != null ? qIds.ToList() : new List<int>();
        }

        public List<Order> GetOrders(int memberId = 0)
        {
            var orders = new List<Order>();

            var qOrders = from order in this.DataContext.Orders
                          where (memberId > 0 ? order.MemberId == memberId : true)
                          select order;

            orders = qOrders != null ? qOrders.ToList() : new List<Order>();

            foreach (var order in qOrders)
            {
                var qItems = from orderItem in this.DataContext.OrderItems
                            where orderItem.OrderId == order.OrderId
                            select orderItem;

                order.OrderItems = qItems != null ? qItems.ToList() : new List<OrderItem>();
            }

            return orders;
        }

        public Order GetOrder(int orderId)
        {
            Order order = null;
            var qOrder = from o in this.DataContext.Orders
                         where o.OrderId == orderId
                         select o;

            order = qOrder.FirstOrDefault<Order>();

            if (order != null)
            {
                var items = from orderItem in this.DataContext.OrderItems
                            where orderItem.OrderId == order.OrderId
                            select orderItem;

                order.OrderItems = items != null ? items.ToList() : new List<OrderItem>();
            }

            return order;
        }
    }
}