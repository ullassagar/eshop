using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class CartHandler
    {
        public static Cart GetCart(int memberId)
        {
            return CartRepository.GetCart(memberId);
        }

        public static void SaveCart(Cart cart)
        {
            CartRepository.SaveCart(cart);
        }

        public static void AddProduct(int productId, int productCount, Cart cart)
        {
            var product = ProductHandler.GetProduct(productId);
            if (product != null)
            {
                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<CartItem>();
                }

                var item = cart.CartItems.Find(p => p.ProductId == productId);
                if (item != null)
                {
                    item.ProductCount += productCount;
                }
                else
                {
                    var newItem = CartItem(product, productCount);
                    cart.CartItems.Add(newItem);
                }

                SaveCart(cart);
            }
        }

        public static void ChangeProductCount(int productId, int productCount, Cart cart)
        {
            var product = ProductHandler.GetProduct(productId);
            if (product != null)
            {
                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<CartItem>();
                }

                var item = cart.CartItems.Find(p => p.ProductId == productId);
                if (item != null)
                {
                    item.ProductCount = productCount;
                    if (productCount == 0)
                    {
                        cart.CartItems.Remove(item);
                    }
                }

                SaveCart(cart);
            }
        }

        private static CartItem CartItem(Product product, int productCount)
        {
            var newItem = new CartItem
            {
                ProductId = product.ProductId,
                ProductCount = productCount,
                ProductName = product.ProductName,
                ImageUrl = product.ImageUrl,
                PriceOut = product.Price,
                Cbm=product.Cbm
            };
            return newItem;
        }
    }
}
