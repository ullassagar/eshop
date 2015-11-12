using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLayer;

namespace DataLayer
{
   public class Order
    {
       public int OrderId { get; set; }
       public int MemberId { get; set; }

       public List<OrderItem> OrderItems { get; set; }

       public static Order Load(IDataReader reader)
       {
           var order = new Order();
           order.OrderId = DbHelper.ConvertToInt32(reader["OrderId"]);
           order.MemberId = DbHelper.ConvertToInt32(reader["MemberId"]);

           return order;
       }
    }
}
