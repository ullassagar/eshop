using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderOrderStatus
    {
        [Key]
        public int OrderOrderStatusId { get; set; }
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
