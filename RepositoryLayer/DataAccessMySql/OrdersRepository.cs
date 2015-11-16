using System;
using System.Collections.Generic;
using System.Data;
using DataLayer;

namespace RepositoryLayer
{
    public class OrdersRepository : IOrdersRepository
    {
        public int ConfirmOrder(ref Cart cart)
        {
            var orderId = 0;

            using (var connection = MysqlRepository.GetConnection_Writable())
            {
                var transaction = connection.BeginTransaction();

                var sql = string.Format(@"INSERT INTO Orders(MemberId, OrderValue, CreationDate) VALUES({0}, {1}, NOW()); SELECT LAST_INSERT_ID();", cart.MemberId, cart.CartTotalPriceOut);
                orderId = Convert.ToInt32(MysqlRepository.ExecuteScalarWithOpenConnection(connection, transaction, sql, null));

                sql = string.Format("INSERT INTO OrderOrderStatus(OrderId, OrderStatusId, CreationDate) VALUES({0}, {1}, NOW()); SELECT LAST_INSERT_ID();", orderId, (int)OrderStatus.Confirmed);
                var orderOrderStatusId = Convert.ToInt32(MysqlRepository.ExecuteScalarWithOpenConnection(connection, transaction, sql, null));

                sql = string.Format("UPDATE Orders SET LatestOrderStatusId={0} WHERE OrderId={1};", orderOrderStatusId, orderId);
                MysqlRepository.ExecuteNonQueryAndKeepConnection(connection, transaction, sql, null);

                if (orderId > 0 && cart.Items != null)
                {
                    foreach (var item in cart.Items)
                    {
                        sql = string.Format(@"INSERT INTO orderdetails(OrderId, ProductId, ProductCount, PriceOut, TotalPriceOut, CreationDate) VALUES({0}, {1}, {2}, {3}, {4}, NOW())", orderId, item.ProductId, item.ProductCount, item.PriceOut, item.TotalPriceOut);
                        MysqlRepository.ExecuteNonQueryAndKeepConnection(connection, transaction, sql, null);
                    }
                }

                transaction.Commit();

                ClearCart(ref cart);
            }

            return orderId;
        }

        private void ClearCart(ref Cart cart)
        {
            cart = new Cart { Id = cart.Id, MemberId = cart.MemberId };
        }

        public List<int> GetOrderIds(int memberId)
        {
            var list = new List<int>();
            var sql = string.Format(@"SELECT OrderId FROM orders WHERE MemberId = {0}", memberId);
            var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql);
            while (reader.Read())
            {
                list.Add(Convert.ToInt32(reader["orderid"]));
            }
            return list;
        }

        public List<Order> GetOrders(int memberId = 0)
        {
            var list = new List<Order>();

            using (var connection = MysqlRepository.GetConnection_Writable())
            {
                var sql = @" SELECT O.OrderId, O.MemberId, OOS.OrderStatusId, O.OrderValue, O.CreationDate
                FROM Orders O, OrderOrderStatus OOS
                WHERE 1=1 AND O.OrderId=OOS.OrderId AND O.LatestOrderStatusId=OOS.OrderOrderStatusId ";

                if (memberId > 0)
                {
                    sql += string.Format(" AND MemberId={0} ", memberId);
                }

                var reader = MysqlRepository.ExecuteReader(connection, sql, null, true);
                using (reader)
                {
                    while (reader.Read())
                    {
                        list.Add(Order.Load(reader));
                    }
                }

                foreach (var order in list)
                {
                    order.OrderItems = new List<OrderItem>();
                    sql = string.Format(@"SELECT OD.OrderDetailId, OD.OrderId, P.ProductId, P.ProductName, P.ImageUrl, P.Length, P.Width, P.Height, OD.ProductCount, OD.PriceOut, OD.TotalPriceOut
                    FROM OrderDetails OD, Products P
                    WHERE OD.RemovedDate IS NULL
                    AND OD.ProductId=P.ProductId
                    AND OD.OrderId={0}", order.OrderId);

                    reader = MysqlRepository.ExecuteReader(connection, sql, null, true);
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            order.OrderItems.Add(OrderItem.Load(reader));
                        }
                    }
                }
            }

            return list;
        }

        public Order GetOrder(int orderId)
        {
            Order order = null;
            if (orderId > 0)
            {
                using (var connection = MysqlRepository.GetConnection_Writable())
                {
                    var sql = @" SELECT O.OrderId, O.MemberId, OOS.OrderStatusId, O.OrderValue, O.CreationDate
                    FROM Orders O, OrderOrderStatus OOS
                    WHERE 1=1 AND O.OrderId=OOS.OrderId AND O.LatestOrderStatusId=OOS.OrderOrderStatusId ";

                    if (orderId > 0)
                    {
                        sql += string.Format(" AND O.OrderId={0} ", orderId);
                    }

                    var reader = MysqlRepository.ExecuteReader(connection, sql, null, true);
                    using (reader)
                        if (reader.Read())
                        {
                            order = Order.Load(reader);
                        }

                    if (order != null)
                    {
                        order.OrderItems = new List<OrderItem>();
                        sql = string.Format(@"SELECT OD.OrderDetailId, OD.OrderId, P.ProductId, P.ProductName, P.ImageUrl, P.Length, P.Width, P.Height, OD.ProductCount, OD.PriceOut, OD.TotalPriceOut
                        FROM OrderDetails OD, Products P
                        WHERE OD.RemovedDate IS NULL
                        AND OD.ProductId=P.ProductId
                        AND OD.OrderId={0}", order.OrderId);

                        reader = MysqlRepository.ExecuteReader(connection, sql, null, true);
                        using (reader)
                            while (reader.Read())
                            {
                                order.OrderItems.Add(OrderItem.Load(reader));
                            }
                    }
                }
            }
            return order;
        }
    }
}