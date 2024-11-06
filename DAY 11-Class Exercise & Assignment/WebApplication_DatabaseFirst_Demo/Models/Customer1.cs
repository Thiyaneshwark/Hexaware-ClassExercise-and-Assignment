using System;
using System.Collections.Generic;

namespace WebApplication_DatabaseFirst_Demo.Models
{
    public partial class Customer1
    {
        public Customer1()
        {
            Order1s = new HashSet<Order1>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public virtual ICollection<Order1> Order1s { get; set; }
    }
}
