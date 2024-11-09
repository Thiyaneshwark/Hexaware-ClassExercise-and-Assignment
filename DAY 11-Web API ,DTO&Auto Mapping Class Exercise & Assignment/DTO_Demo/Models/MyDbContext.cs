using Microsoft.EntityFrameworkCore;

namespace DTO_Demo.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { } 
        public DbSet<Book> Books { get; set; }
    }
}
