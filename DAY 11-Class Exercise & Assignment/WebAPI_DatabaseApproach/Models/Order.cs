using System;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? ShippingAddress { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
