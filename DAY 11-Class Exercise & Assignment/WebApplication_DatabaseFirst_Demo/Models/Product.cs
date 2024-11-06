using System;
using System.Collections.Generic;

namespace WebApplication_DatabaseFirst_Demo.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
