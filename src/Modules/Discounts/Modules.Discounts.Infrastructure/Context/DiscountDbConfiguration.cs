using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Infrastructure.Context
{
    internal class DiscountDbConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
        }      

    }
}
