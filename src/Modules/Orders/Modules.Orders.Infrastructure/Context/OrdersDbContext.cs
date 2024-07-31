using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.Context
{
    internal class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
            
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new BasketDbConfiguration());
        //    modelBuilder.ApplyConfiguration(new BasketItemDbConfiguration());
        //}
    }
}
