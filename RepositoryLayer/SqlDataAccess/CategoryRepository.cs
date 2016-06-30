using DataLayer;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer
{
    public class CategoryRepository : GenericSqlRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}