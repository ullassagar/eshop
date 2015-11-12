using System.Collections.Generic;
using DataLayer;

namespace WebApp.Models.Shopping
{
    public class CartModel : MasterModel
    {
        public int MemberId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public int CartProductCount { get; set; }
        public decimal CartTotalCbm { get; set; }
        public decimal CartTotalPriceOut { get; set; }

        public CartModel()
        {
            CartItems = new List<CartItemModel>();
        }
    }

    public class CartModelMapper
    {
        public static CartModel Map(Cart cart)
        {
            var model = new CartModel();
            if (cart != null)
            {
                model.MemberId = cart.MemberId;
                model.CartProductCount = cart.CartProductCount;
                model.CartTotalCbm = cart.CartTotalCbm;
                model.CartTotalPriceOut = cart.CartTotalPriceOut;

                if (cart.Items != null)
                {
                    foreach (var cartItem in cart.Items)
                    {
                        model.CartItems.Add(CartItemModelMapper.Map(cartItem));
                    }
                }
            }
            return model;
        }
    }
}