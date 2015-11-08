using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class OrderHandler
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderHandler()
        {
            _ordersRepository = new OrdersRepository();
        }

        public int AddOrder(Cart cart)
        {
            return _ordersRepository.AddOrder(cart);
        }

        public List<int> GetOrderIds(int memberId)
        {
            return _ordersRepository.GetOrderIds(memberId);
        }
    }
}