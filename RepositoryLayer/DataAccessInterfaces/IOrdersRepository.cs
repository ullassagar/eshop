using DataLayer;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public interface IOrdersRepository
    {
        int ConfirmOrder(ref Cart cart);
        List<int> GetOrderIds(int memberId);
    }
}