using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.ValueObjects;

namespace Modules.Baskets.Infrastructure.Context
{
    internal class BasketDbConfiguration : IEntityTypeConfiguration<Basket>, IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new BasketId(c));

            builder.Property(c => c.CustomerId)
                   .HasConversion(c => c.Value, c => new BasketCustomerId(c));

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Basket)
                   .HasForeignKey(c => c.BasketId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Baskets");
        }

        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new BasketItemId(c));

            builder.HasOne(c => c.Basket)
                   .WithMany(p => p.Items)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.OwnsOne(c => c.BaseCurrencyPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("BaseCurrency");
                mv.Property(p => p.Amount).HasColumnName("BaseAmount").HasPrecision(18, 2);
            });

            builder.Property(c => c.ProductId)
                   .HasConversion(c => c.Value, c => new BasketProductId(c));

            builder.Property(c => c.ShopId)
                   .HasConversion(c => c.Value, c => new BasketShopId(c));

            builder.Property(c => c.Quantity).HasColumnName("Quantity");

            builder.ToTable("BasketItems");
        }
    }
}
