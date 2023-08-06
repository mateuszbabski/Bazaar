using Microsoft.EntityFrameworkCore;
using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Infrastructure.Context.ShippingMethods
{
    internal class ShippingMethodsDbContext : DbContext
    {
        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        public ShippingMethodsDbContext(DbContextOptions<ShippingMethodsDbContext> options)
            : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShippingMethodDbConfiguration());
        }
    }
}
