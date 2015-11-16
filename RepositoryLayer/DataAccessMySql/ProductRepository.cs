using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer;

namespace RepositoryLayer
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetList(bool includeOutOfStock = true)
        {
            var productList = new List<Product>();
            var sb = new StringBuilder();
            sb.Append("SELECT ProductId, ProductName, EanCode, ImageUrl, Price, Length, Width, Height, Description, IsOutOfStock FROM Products");

            if (!includeOutOfStock)
            {
                sb.AppendFormat("  WHERE IsOutOfStock=False ");
            }

            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sb.ToString()))
            {
                while (reader.Read())
                {
                    productList.Add(Product.Load(reader));
                }
            }
            return productList;
        }

        public Product GetProduct(int productId)
        {
            Product product = null;
            var sql = string.Format(@"SELECT ProductId, ProductName, EanCode, ImageUrl, Price, Length, Width, Height, Description, IsOutOfStock FROM Products Where ProductId={0}", productId);
            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    product = Product.Load(reader);
                }
            }
            return product;
        }

        public void Add(Product product)
        {
            var sql = string.Format(@"INSERT INTO products(ProductName, EanCode, ImageUrl, Price, Length, Width, Height, Description, IsOutOfStock)
            VALUES('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}', {8});Select last_insert_id();",
            product.ProductName, product.EanCode, product.ImageUrl,
            product.Price, product.Length, product.Width, product.Height, product.Description, product.IsOutOfStock);
            product.ProductId = Convert.ToInt32(MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null));
        }

        public void Update(Product product)
        {
            var sql = string.Format(@"UPDATE products
            SET ProductName='{0}', EanCode='{1}', ImageUrl='{2}',
            Price={3}, Length={4}, Width={5}, Height={6}, Description='{7}',  IsOutOfStock={8} WHERE ProductId={9}",
            product.ProductName, product.EanCode, product.ImageUrl, product.Price,
            product.Length, product.Width, product.Height, product.Description, product.IsOutOfStock, product.ProductId);
            MysqlRepository.ExecuteNonQueryAndCloseConnection(MysqlRepository.ConnectionString_Writable, sql, null);
        }
    }
}