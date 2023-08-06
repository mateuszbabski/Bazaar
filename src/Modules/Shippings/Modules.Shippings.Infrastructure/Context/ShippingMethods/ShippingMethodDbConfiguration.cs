using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.ValueObjects;

namespace Modules.Shippings.Infrastructure.Context.ShippingMethods
{
    internal sealed class ShippingMethodDbConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShippingMethodId(c));

            builder.Property(c => c.Name)
                   .HasConversion(c => c.Value, c => new ShippingMethodName(c));

            builder.Property(c => c.DurationTimeInDays).HasColumnName("DurationTimeInDays");

            builder.Property(c => c.IsAvailable).HasColumnName("IsAvailable");

            builder.OwnsOne(c => c.BasePrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("BaseCurrency");
                mv.Property(p => p.Amount).HasColumnName("BaseAmount").HasPrecision(18, 2);
            });

            builder.ToTable("ShippingMethods");
        }
    }
}
