using API_CodeFirst_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CodeFirst_Demo.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
