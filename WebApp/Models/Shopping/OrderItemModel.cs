using DataLayer;

namespace WebApp.Models.Shopping
{
    public class OrderItemModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public decimal PriceOut { get; set; }
        public decimal TotalPriceOut { get; set; }

        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Cbm { get; set; }
        public decimal TotalCbm { get; set; }
    }

    public static class OrderItemModelMapper
    {
        public static OrderItemModel Map(OrderItem item)
        {
            var model = new OrderItemModel();
            if (item != null)
            {
                model.OrderDetailId = item.OrderDetailId;
                model.OrderId = item.OrderId;
                model.ProductId = item.ProductId;
                model.ProductCount = item.ProductCount;
                model.PriceOut = item.PriceOut;
                model.TotalPriceOut = item.TotalPriceOut;

                model.ProductName = item.ProductName;
                model.ImageUrl = item.ImageUrl;
                model.Length = item.Length;
                model.Width = item.Width;
                model.Height = item.Height;
                model.Cbm = item.Cbm;
                model.TotalCbm = item.TotalCbm;
            }
            return model;
        }
    }
}