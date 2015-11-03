using System;
using System.Collections.Generic;
using System.Data;
using DataLayer;
using MySql.Data.MySqlClient;

namespace RepositoryLayer
{
    public class ProductRepository
    {
        public static List<Product> GetList()
        {
            var productList = new List<Product>();

            string sql = string.Format(@"SELECT ProductId, ProductName, EanCode, ImageUrl, Price, Length, Width, Height, Description FROM Products;");

            using (MySqlDataReader reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    productList.Add(Product.Load(reader));
                }
            }

            return productList;
        }


        public static Product GetProduct(int productId)
        {
            Product product = null;

            string sql = string.Format(@"SELECT ProductId, ProductName, EanCode, ImageUrl, Price, Length, Width, Height, Description FROM Products
                                      Where ProductId={0}", productId);

            using (MySqlDataReader reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    product = Product.Load(reader);
                }
            }
            return product;
        }


        public static void Add(Product product)
        {
            string sql = string.Format(@"INSERT INTO products(ProductName, EanCode, ImageUrl, Price, Length, Width, Height, Description)
            VALUES('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}');Select last_insert_id();",
            product.ProductName, product.EanCode, product.ImageUrl,
            product.Price, product.Length, product.Width, product.Height, product.Description);

            product.ProductId = Convert.ToInt32(MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null));
        }

        public static void Update(Product product)
        {
            string sql = string.Format(@"UPDATE products
            SET ProductName='{0}', EanCode='{1}', ImageUrl='{2}',
            Price={3}, Length={4}, Width={5}, Height={6}, Description='{7}' WHERE ProductId={8}",
            product.ProductName, product.EanCode,product.ImageUrl, product.Price,
            product.Length, product.Width, product.Height,product.Description, product.ProductId);

            MysqlRepository.ExecuteNonQueryAndCloseConnection(MysqlRepository.ConnectionString_Writable, sql, null);
        }

        public static void Delete(int id)
        {
            string sql = string.Format(@"DELETE FROM products WHERE  ProductId = {0}", id);
            MysqlRepository.ExecuteNonQueryAndCloseConnection(MysqlRepository.ConnectionString_ReadOnly, sql);
        }
    }
}