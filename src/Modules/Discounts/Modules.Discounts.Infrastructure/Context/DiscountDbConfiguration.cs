using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Infrastructure.Context
{
    internal class DiscountDbConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new DiscountId(c));

            builder.Property(c => c.DiscountValue).HasColumnName("DiscountValue").IsRequired();

            builder.Property(c => c.IsPercentageDiscount).HasColumnName("IsPercentageDiscount");

            builder.Property(c => c.Currency).HasColumnName("Currency");

            builder.OwnsOne(c => c.DiscountTarget, mv =>
            {
                mv.Property(p => p.DiscountType).HasColumnName("DiscountType");
                mv.Property(p => p.TargetId).HasColumnName("TargetId");
            });

            builder.HasMany<DiscountCoupon>(c => c.DiscountCoupons)
                   .WithOne(p => p.Discount)
                   .HasForeignKey(p => p.DiscountId);

            builder.ToTable("Discounts");
        }      

    }
}
