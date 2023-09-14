using Microsoft.EntityFrameworkCore;
using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Infrastructure.Context
{
    internal class DiscountsDbContext : DbContext
    {
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountCoupon> DiscountCoupons { get; set; }

        public DiscountsDbContext(DbContextOptions<DiscountsDbContext> options)
            : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiscountDbConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountCouponDbConfiguration());
        }
    }
}
