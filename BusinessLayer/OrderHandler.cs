using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class OrderHandler
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
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