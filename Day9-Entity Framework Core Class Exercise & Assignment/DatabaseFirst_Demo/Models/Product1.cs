using System;
using System.Collections.Generic;

namespace DatabaseFirst_Demo.Models
{
    public partial class Product1
    {
        public Product1()
        {
            OrderItem1s = new HashSet<OrderItem1>();
            Stocks = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderItem1> OrderItem1s { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
