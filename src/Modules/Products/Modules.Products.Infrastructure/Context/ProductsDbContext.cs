using Microsoft.EntityFrameworkCore;
using Modules.Products.Domain.Entities;

namespace Modules.Products.Infrastructure.Context
{
    internal class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductDbConfiguration());
        }
    }
}
