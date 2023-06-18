using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Shops.Infrastructure.Context
{
    internal class ShopDbConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            var passwordConverter = new ValueConverter<PasswordHash, string>(c => c.Value, c => new PasswordHash(c));

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new ShopId(c));

            builder.Property(c => c.Email)
                   .HasConversion(c => c.Value, c => new Email(c));

            builder.Property(c => c.PasswordHash)
                   .HasConversion(passwordConverter);

            builder.Property(c => c.OwnerName)
                   .HasConversion(c => c.Value, c => new Name(c));

            builder.Property(c => c.OwnerLastName)
                   .HasConversion(c => c.Value, c => new LastName(c));

            builder.Property(c => c.ShopName)
                   .HasConversion(c => c.Value, c => new ShopName(c));

            builder.Property(c => c.TaxNumber)
                   .HasConversion(c => c.Value, c => new TaxNumber(c));

            builder.OwnsOne(c => c.ShopAddress, sa =>
            {
                sa.Property(x => x.Country).HasColumnName("Country").IsRequired();
                sa.Property(x => x.City).HasColumnName("City").IsRequired();
                sa.Property(x => x.Street).HasColumnName("Street").IsRequired();
                sa.Property(x => x.PostalCode).HasColumnName("PostalCode").IsRequired();
            });

            builder.Property(c => c.ContactNumber)
                   .HasConversion(c => c.Value, c => new TelephoneNumber(c));

            builder.Property(c => c.Role)
                   .HasColumnName("Role");

            builder.ToTable("Shops");
        }
    }
}
