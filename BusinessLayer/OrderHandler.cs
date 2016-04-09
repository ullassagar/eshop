using System;
using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

namespace BusinessLayer
{
    public class OrderHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderOrderStatusRepository _orderOrderStatusRepository;

        public OrderHandler(IOrdersRepository ordersRepository, IOrderItemRepository orderItemRepository, IOrderOrderStatusRepository orderOrderStatusRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ordersRepository = ordersRepository;
            _orderItemRepository = orderItemRepository;
            _orderOrderStatusRepository = orderOrderStatusRepository;
        }

        public int ConfirmOrder(ref Cart cart)
        {
            var orderId = 0;
            using (var transacton = _ordersRepository.DataContext.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order { MemberId = cart.MemberId, OrderValue = cart.CartTotalPriceOut, CreationDate = DateTime.Now };
                    _ordersRepository.Add(order);
                    _ordersRepository.DataContext.SaveChanges();

                    var orderOrderStatus = new OrderOrderStatus { OrderId = order.OrderId, OrderOrderStatusId = (int)OrderStatusType.Confirmed, CreationDate = DateTime.Now };
                    _orderOrderStatusRepository.Add(orderOrderStatus);
                    _orderOrderStatusRepository.DataContext.SaveChanges();

                    order.LatestOrderStatusId = orderOrderStatus.OrderOrderStatusId;
                    _ordersRepository.DataContext.SaveChanges();

                    if (order.OrderId > 0 && cart.Items != null)
                    {
                        foreach (var item in cart.Items)
                        {
                            var orderItem = new OrderItem
                            {
                                OrderId = order.OrderId,
                                ProductId = item.ProductId,
                                ProductCount = item.ProductCount,
                                PriceOut = item.PriceOut,
                                TotalPriceOut = item.TotalPriceOut,
                               CreationDate = DateTime.Now 
                            };

                            _orderItemRepository.Add(orderItem);
                            _orderItemRepository.DataContext.SaveChanges();
                        }
                    }

                    transacton.Commit();

                    ClearCart(ref cart);

                    orderId = order.OrderId;
                }
                catch (Exception)
                {
                    transacton.Rollback();
                }
            }

            return orderId;
        }

        private void ClearCart(ref Cart cart)
        {
            cart = new Cart { Id = cart.Id, MemberId = cart.MemberId };
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