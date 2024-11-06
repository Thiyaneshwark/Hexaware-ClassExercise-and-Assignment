using System;
using System.Collections.Generic;

namespace WebApplication_DatabaseFirst_Demo.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
