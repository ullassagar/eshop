using DataLayer;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public interface IOrdersRepository : Repositories.IRepository<Order>
    {
        List<int> GetOrderIds(int memberId);
        List<Order> GetOrders(int memberId = 0);
        Order GetOrder(int orderId);
    }
}