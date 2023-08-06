using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.ValueObjects;

namespace Modules.Shippings.Infrastructure.Context.Shippings
{
    internal sealed class ShippingDbConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShippingId(c));

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.Property(c => c.OrderId)
                   .HasConversion(c => c.Value, c => new ShippingOrderId(c));

            builder.Property(c => c.ShippingMethodId)
                   .HasConversion(c => c.Value, c => new ShippingMethodId(c));

            builder.Property(c => c.TrackingNumber)
                   .HasConversion(c => c.Value, c => new TrackingNumber(c));

            builder.Property(c => c.Status).HasColumnName("ShippingStatus");
            builder.Property(c => c.TotalWeight).HasColumnName("TotalWeight");
            builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(c => c.LastUpdateDate).HasColumnName("LastUpdatedDate");

            builder.ToTable("Shippings");
        }
    }
}
