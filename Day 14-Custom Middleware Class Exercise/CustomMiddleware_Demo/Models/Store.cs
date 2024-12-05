using System;
using System.Collections.Generic;

namespace CustomMiddleware_Demo.Models
{
    public partial class Store
    {
        public Store()
        {
            Order1s = new HashSet<Order1>();
            Stocks = new HashSet<Stock>();
            staff = new HashSet<Staff>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public virtual ICollection<Order1> Order1s { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Staff> staff { get; set; }
    }
}
