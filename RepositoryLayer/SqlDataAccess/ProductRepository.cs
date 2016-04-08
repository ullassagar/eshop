using System.Collections.Generic;
using System.Linq;
using DataLayer;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer
{
    public class ProductRepository : GenericSqlRepository<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<Product> GetList(bool includeOutOfStock = true)
        {
            return this.GetMany(p => includeOutOfStock || p.IsOutOfStock == false).ToList();
        }

        public Product GetProduct(int productId)
        {
            return this.GetById(productId);
        }
    }
}