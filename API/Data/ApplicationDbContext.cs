using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Department> Department { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
