using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public enum OrderStatusType
    {
        UnKnown = 0,
        Confirmed = 1,
        BeingPicked = 2,
        Picked = 3,
        Delivered = 4,
        Cancelled = 5
    }

    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
