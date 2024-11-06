using System;
using System.Collections.Generic;

namespace Assignment_DBApproach.Models
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Book>? Books { get; set; }
    }
}
