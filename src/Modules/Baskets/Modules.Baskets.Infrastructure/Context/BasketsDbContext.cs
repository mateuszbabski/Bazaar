using Microsoft.EntityFrameworkCore;
using Modules.Baskets.Domain.Entities;

namespace Modules.Baskets.Infrastructure.Context
{
    internal class BasketsDbContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public BasketsDbContext(DbContextOptions<BasketsDbContext> options)
            : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BasketDbConfiguration());
            modelBuilder.ApplyConfiguration(new BasketItemDbConfiguration());
        }
    }
}
