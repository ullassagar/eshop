using DataLayer;

namespace WebApp.Models
{
    public class CartItemModel
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
        public static CartItemModel Map(CartItem item)
        {
            var model = new CartItemModel();
            if (item != null)
            {
                model.ProductId = item.ProductId;
                model.ProductName = item.ProductName;
                model.ImageUrl = item.ImageUrl;
                model.PriceOut = item.PriceOut;
                model.Vat = item.Vat;
                model.Cbm = item.Cbm;
                model.ProductCount = item.ProductCount;
                model.TotalCbm = item.TotalCbm;
                model.TotalPriceOut = item.TotalPriceOut;
            }
            return model;
        }
    }
}