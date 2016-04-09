using DataLayer;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer
{
    public class OrderOrderStatusRepository : GenericSqlRepository<OrderOrderStatus>, IOrderOrderStatusRepository
    {
        public OrderOrderStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}