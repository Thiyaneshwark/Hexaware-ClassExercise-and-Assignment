using Assignment_Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Authentication.Authentication
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
