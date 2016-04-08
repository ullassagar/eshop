using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

namespace BusinessLayer
{
    public class OrderHandler
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public int ConfirmOrder(ref Cart cart)
        {
            return _ordersRepository.ConfirmOrder(ref cart);
        }

        public List<int> GetOrderIds(int memberId)
        {
            return _ordersRepository.GetOrderIds(memberId);
        }

        public List<Order> GetOrders(int memberId = 0)
        {
            return _ordersRepository.GetOrders(memberId);
        }

        public Order GetOrder(int orderId)
        {
            return _ordersRepository.GetOrder(orderId);
        }
    }
}