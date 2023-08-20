using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Infrastructure.Context
{
    internal class DiscountCouponDbConfiguration : IEntityTypeConfiguration<DiscountCoupon>
    {
        public void Configure(EntityTypeBuilder<DiscountCoupon> builder)
        {
            builder.ToTable("DiscountCoupons");
        }
    }
}
