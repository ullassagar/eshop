using DataLayer;
using System.Collections.Generic;

namespace RepositoryLayer
{
    public interface IProductRepository
    {
        List<Product> GetList();
        Product GetProduct(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}