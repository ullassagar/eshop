﻿using System.Collections.Generic;
using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class CartHandler
    {
        private readonly ICartRepository _cartRepository;
        private readonly ProductHandler _productHandler;

        public CartHandler(ICartRepository cartRepository, ProductHandler productHandler)
        {
            _cartRepository = cartRepository;
            _productHandler = productHandler;
        }

        public Cart GetCart(int memberId)
        {
            return _cartRepository.GetCart(memberId);
        }

        public void SaveCart(Cart cart)
        {
            _cartRepository.SaveCart(cart);
        }

        public void AddProduct(int productId, int productCount, Cart cart)
        {
            var product = _productHandler.GetProduct(productId);
            if (product != null)
            {
                if (cart.Items == null)
                {
                    cart.Items = new List<CartItem>();
                }

                var item = cart.Items.Find(p => p.ProductId == productId);
                if (item != null)
                {
                    item.ProductCount += productCount;
                }
                else
                {
                    var newItem = CartItem(product, productCount);
                    cart.Items.Add(newItem);
                }

                SaveCart(cart);
            }
        }

        public void ChangeProductCount(int productId, int productCount, Cart cart)
        {
            var product = _productHandler.GetProduct(productId);
            if (product != null)
            {
                if (cart.Items == null)
                {
                    cart.Items = new List<CartItem>();
                }

                var item = cart.Items.Find(p => p.ProductId == productId);
                if (item != null)
                {
                    item.ProductCount = productCount;
                    if (productCount == 0)
                    {
                        cart.Items.Remove(item);
                    }
                }

                SaveCart(cart);
            }
        }

        private CartItem CartItem(Product product, int productCount)
        {
            var newItem = new CartItem
            {
                ProductId = product.ProductId,
                ProductCount = productCount,
                ProductName = product.ProductName,
                ImageUrl = product.ImageUrl,
                PriceOut = product.Price,
                Cbm = product.Cbm
            };
            return newItem;
        }
    }
}
