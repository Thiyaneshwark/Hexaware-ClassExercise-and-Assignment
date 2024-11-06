using System;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? StockQuantity { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
