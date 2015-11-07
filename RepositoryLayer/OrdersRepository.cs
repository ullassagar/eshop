using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class OrdersRepository
    {
        public static int AddOrder(Cart cart)
        {
            var sql = string.Format(@"INSERT INTO orders(MemberId) VALUES({0});select last_insert_id();", cart.MemberId);

            int orderId = Convert.ToInt32(MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null));

            if (orderId > 0 && cart.CartItems != null)
            {
                foreach (var item in cart.CartItems)
                {
                    sql = string.Format(@"INSERT INTO orderdetails(OrderId, ProductId, ProductCount) 
                                          VALUES({0},{1},{2})", orderId, item.ProductId, item.ProductCount);
                    MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null);
                }
            }
            return orderId;
        }


        public static List<int> GetOrderIds(int memberId)
        {
            List<int> list = new List<int>();
            var sql = string.Format(@"SELECT OrderId FROM orders WHERE MemberId = {0}");
            var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql);
            while (reader.Read())
            {
                list.Add(Convert.ToInt32(reader["orderid"]));
            }
            return list;
        }
    }
}
