using DataLayer;


namespace WebApp.Models
{
    public class OrderDetailModel
    {
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }

    public static class OrderDetailModelMapper
    {
        public static OrderDetailModel Map(OrderDetail item)
        {
            var model = new OrderDetailModel();
            if (item != null)
            {
                model.ProductId = item.ProductId;
                model.ProductCount = item.ProductCount;
            }
            return model;
        }
    }
}