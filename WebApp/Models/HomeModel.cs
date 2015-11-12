using System.Collections.Generic;
using WebApp.Models.Products;

namespace WebApp.Models
{
    public class HomeModel : MasterModel
    {
        public List<ProductModel> Products { get; set; }

        public HomeModel()
        {
            Products = new List<ProductModel>();
        }
    }
}