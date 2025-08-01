using CompanyPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
