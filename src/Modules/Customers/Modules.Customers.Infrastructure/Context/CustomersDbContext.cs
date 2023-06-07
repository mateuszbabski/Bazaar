using Microsoft.EntityFrameworkCore;
using Modules.Customers.Domain.Entities;

namespace Modules.Customers.Infrastructure.Context
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
            : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
        }
    }
}
