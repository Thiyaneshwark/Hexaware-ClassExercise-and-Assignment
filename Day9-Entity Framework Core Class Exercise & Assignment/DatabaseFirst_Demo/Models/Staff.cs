using System;
using System.Collections.Generic;

namespace DatabaseFirst_Demo.Models
{
    public partial class Staff
    {
        public Staff()
        {
            InverseManager = new HashSet<Staff>();
            Order1s = new HashSet<Order1>();
        }

        public int StaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Staff? Manager { get; set; }
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<Staff> InverseManager { get; set; }
        public virtual ICollection<Order1> Order1s { get; set; }
    }
}
