using DataLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer
{
    public class OrdersRepository : IOrdersRepository
    {
        public int ConfirmOrder(ref Cart cart)
        {
            string sql = string.Format(@"INSERT INTO orders(MemberId) VALUES({0});select last_insert_id();", cart.MemberId);
            int orderId = Convert.ToInt32(MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null));
            if (orderId > 0 && cart.Items != null)
            {
                foreach (CartItem item in cart.Items)
                {
                    sql = string.Format(@"INSERT INTO orderdetails(OrderId, ProductId, ProductCount) 
                                          VALUES({0},{1},{2})", orderId, item.ProductId, item.ProductCount);
                    MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null);
                }
            }
            
            ClearCart(ref cart);

            return orderId;
        }

        private void ClearCart(ref Cart cart)
        {
            cart = new Cart { Id = cart.Id };
        }

        public List<int> GetOrderIds(int memberId)
        {
            var list = new List<int>();
            string sql = string.Format(@"SELECT OrderId FROM orders WHERE MemberId = {0}", memberId);
            MySqlDataReader reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql);
            while (reader.Read())
            {
                list.Add(Convert.ToInt32(reader["orderid"]));
            }
            return list;
        }
    }
}