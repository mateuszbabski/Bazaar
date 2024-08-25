using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Infrastructure.Context
{
    internal class DiscountCouponDbConfiguration : IEntityTypeConfiguration<DiscountCoupon>
    {
        public void Configure(EntityTypeBuilder<DiscountCoupon> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new DiscountCouponId(c));

            builder.Property(c => c.DiscountId)
                   .HasConversion(c => c.Value, c => new DiscountId(c));

            builder.Property(c => c.CreatedBy).IsRequired();

            builder.HasOne<Discount>(c => c.Discount)
                   .WithMany(p => p.DiscountCoupons)
                   .HasForeignKey(c => c.DiscountId);

            builder.Property(c => c.DiscountCode).HasColumnName("DiscountCode").IsRequired();

            builder.Property(c => c.StartsAt).HasColumnName("StartsAt");

            builder.Property(c => c.ExpirationDate).HasColumnName("ExpirationDate");

            builder.Property(c => c.IsEnable);

            builder.ToTable("DiscountCoupons");
        }
    }
}
