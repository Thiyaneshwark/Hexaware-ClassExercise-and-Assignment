using System;
using System.Collections.Generic;

namespace Assignment_DBApproach.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Book>? Books { get; set; }
    }
}
