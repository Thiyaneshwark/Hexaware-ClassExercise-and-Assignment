using System;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
