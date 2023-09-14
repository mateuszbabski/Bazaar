using Microsoft.EntityFrameworkCore;
using Modules.Shops.Domain.Entities;

namespace Modules.Shops.Infrastructure.Context
{
    internal class ShopsDbContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; } 

        public ShopsDbContext(DbContextOptions<ShopsDbContext> options)
            : base(options)
        {
            
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShopDbConfiguration());
        }
    }
}
