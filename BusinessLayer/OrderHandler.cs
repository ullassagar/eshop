using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer;

namespace BusinessLayer
{
    public class OrderHandler
    {
        public static int AddOrder(Cart cart)
        {
            return OrdersRepository.AddOrder(cart);
        }

        public static List<int> GetOrderIds(int memberId)
        {
            return OrdersRepository.GetOrderIds(memberId);
        }
    }
}
