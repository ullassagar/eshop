using DataLayer;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public interface IProductRepository : Repositories.IRepository<Product>
    {
        List<Product> GetList(bool includeOutOfStock = true);
    }

    public interface ICategoryRepository : Repositories.IRepository<Category>
    {
    }
}