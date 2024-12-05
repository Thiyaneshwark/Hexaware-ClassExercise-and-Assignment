using System;
using System.Collections.Generic;

namespace CustomMiddleware_Demo.Models
{
    public partial class OrderItem1
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }

        public virtual Order1 Order { get; set; } = null!;
        public virtual Product1 Product { get; set; } = null!;
    }
}
