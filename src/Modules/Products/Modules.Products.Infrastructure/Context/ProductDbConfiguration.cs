using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Infrastructure.Context
{
    internal class ProductDbConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ProductId(c));

            builder.Property(c => c.ProductName)
                   .HasConversion(c => c.Value, c => new ProductName(c));

            builder.Property(c => c.ProductDescription)
                   .HasConversion(c => c.Value, c => new ProductDescription(c));

            builder.OwnsOne(c => c.ProductCategory, mv =>
            {
                mv.Property(p => p.CategoryName).HasColumnName("ProductCategory").IsRequired();
            });

            builder.Property(c => c.WeightPerUnit)
                   .HasConversion(c => c.Value, c => new Weight(c));

            builder.Property(c => c.Unit)
                   .HasConversion(c => c.Value, c => new ProductUnit(c));

            builder.Property(c => c.ShopId).IsRequired();

            builder.Property(c => c.IsAvailable);

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.ToTable("Products");
        }
    }
}
