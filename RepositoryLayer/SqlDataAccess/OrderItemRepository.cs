using DataLayer;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer
{
    public class OrderItemRepository : GenericSqlRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}