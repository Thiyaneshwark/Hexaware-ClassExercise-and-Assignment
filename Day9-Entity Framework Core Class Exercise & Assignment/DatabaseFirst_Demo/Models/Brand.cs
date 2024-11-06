using System;
using System.Collections.Generic;

namespace DatabaseFirst_Demo.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Product1s = new HashSet<Product1>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;

        public virtual ICollection<Product1> Product1s { get; set; }
    }
}
