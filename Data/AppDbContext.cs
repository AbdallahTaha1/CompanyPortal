using CompanyPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
