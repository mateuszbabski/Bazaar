using Microsoft.EntityFrameworkCore;
using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Infrastructure.Context.Shippings
{
    internal class ShippingsDbContext : DbContext
    {
        public DbSet<Shipping> Shippings { get; set; }

        public ShippingsDbContext(DbContextOptions<ShippingsDbContext> options)
            : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShippingDbConfiguration());
        }
    }
}
