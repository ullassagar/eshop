using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class ProductHandler
    {
        public static List<Product> GetList()
        {
            return ProductRepository.GetList();
        }

        public static void Add(Product product)
        {
            ProductRepository.Add(product);
        }

        public static Product GetProduct(int productId)
        {
            return ProductRepository.GetProduct(productId);
        }

        public static void Update(Product product)
        {
            ProductRepository.Update(product);
        }

        public static void Delete(int id)
        {
            ProductRepository.Delete(id);
        }
    }
}