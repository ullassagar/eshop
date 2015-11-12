using DataLayer;

namespace WebApp.Models.Shopping
{
    public class CartItemModel : MasterModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal PriceOut { get; set; }
        public decimal Vat { get; set; }
        public decimal Cbm { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalCbm { get; set; }
        public decimal TotalPriceOut { get; set; }
    }

    public static class CartItemModelMapper
    {
        public static CartItemModel Map(CartItem cartItem)
        {
            var model = new CartItemModel();
            if (cartItem != null)
            {
                model.ProductId = cartItem.ProductId;
                model.ProductName = cartItem.ProductName;
                model.ImageUrl = cartItem.ImageUrl;
                model.PriceOut = cartItem.PriceOut;
                model.Vat = cartItem.Vat;
                model.Cbm = cartItem.Cbm;
                model.ProductCount = cartItem.ProductCount;
                model.TotalCbm = cartItem.TotalCbm;
                model.TotalPriceOut = cartItem.TotalPriceOut;
            }
            return model;
        }
    }
}