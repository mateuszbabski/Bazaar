using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Baskets.Infrastructure.Context
{
    internal class BasketDbConfiguration : IEntityTypeConfiguration<Basket>
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

            builder.Property(c => c.TotalWeight)
                   .HasConversion(c => c.Value, c => new Weight(c));

            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Basket)
                   .HasForeignKey(c => c.BasketId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Baskets");
        }
    }
}
