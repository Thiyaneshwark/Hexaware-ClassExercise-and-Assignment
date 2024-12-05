using System;
using System.Collections.Generic;

namespace EF_Raw_SQL_Demo.Models
{
    public partial class Order1
    {
        public Order1()
        {
            OrderItem1s = new HashSet<OrderItem1>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StoreId { get; set; }
        public int StaffId { get; set; }

        public virtual Customer1? Customer { get; set; }
        public virtual Staff Staff { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<OrderItem1> OrderItem1s { get; set; }
    }
}
