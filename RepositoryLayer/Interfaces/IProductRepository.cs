using DataLayer;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public interface IProductRepository : Repositories.IRepository<Product>
    {
        List<Product> GetList(bool includeOutOfStock = true);
        Product GetProduct(int productId);
        void Add(Product product);
        void Update(Product product);
    }
}