using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Infrastructure.Context
{
    internal class ShippingDbConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            throw new NotImplementedException();
        }
    }
}
