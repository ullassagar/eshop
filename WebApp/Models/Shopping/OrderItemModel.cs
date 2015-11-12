using DataLayer;

namespace WebApp.Models.Shopping
{
    public class OrderItemModel
    {
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }

    public static class OrderItemModelMapper
    {
        public static OrderItemModel Map(OrderItem item)
        {
            var model = new OrderItemModel();
            if (item != null)
            {
                model.ProductId = item.ProductId;
                model.ProductCount = item.ProductCount;
            }
            return model;
        }
    }
}