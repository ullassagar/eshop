using DataLayer;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public interface IOrdersRepository
    {
        int AddOrder(Cart cart);
        List<int> GetOrderIds(int memberId);
    }
}