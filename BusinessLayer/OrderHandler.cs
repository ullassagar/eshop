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

        public int ConfirmOrder(ref Cart cart)
        {
            return _ordersRepository.ConfirmOrder(ref cart);
        }

        public List<int> GetOrderIds(int memberId)
        {
            return _ordersRepository.GetOrderIds(memberId);
        }
    }
}