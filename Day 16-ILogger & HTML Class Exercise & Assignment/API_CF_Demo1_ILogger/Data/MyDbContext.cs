using API_CF_Demo1.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CF_Demo1.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext()
        {
                
        }
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {
                
        }

        public virtual DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
