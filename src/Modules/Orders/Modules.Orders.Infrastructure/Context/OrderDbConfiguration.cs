using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Infrastructure.Context
{
    internal sealed class OrderDbConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new OrderId(c));

            builder.OwnsOne(c => c.Receiver, mv =>
            {
                mv.Property(c => c.Id).HasConversion(c => c.Value, c=> new ReceiverId(c)).IsRequired().HasColumnName("ReceiverId");
                mv.Property(c => c.Email).HasConversion(c => c.Value, c => new Email(c)).IsRequired().HasColumnName("ReceiverEmail");
                mv.Property(c => c.Name).HasConversion(c => c.Value, c => new Name(c)).IsRequired().HasColumnName("ReceiverName");
                mv.Property(c => c.LastName).HasConversion(c => c.Value, c => new LastName(c)).IsRequired().HasColumnName("ReceiverLastName");
                mv.Property(c => c.TelephoneNumber).HasConversion(c => c.Value, c => new TelephoneNumber(c)).IsRequired().HasColumnName("ReceiverPhoneNumber");

                mv.OwnsOne(c => c.Address, sa =>
                {
                    sa.Property(x => x.Country).HasColumnName("ReceiverCountry").IsRequired();
                    sa.Property(x => x.City).HasColumnName("ReceiverCity").IsRequired();
                    sa.Property(x => x.Street).HasColumnName("ReceiverStreet").IsRequired();
                    sa.Property(x => x.PostalCode).HasColumnName("ReceiverPostalCode").IsRequired();
                });
            });
            

            builder.OwnsOne(c => c.TotalPrice, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Currency");
                mv.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2);
            });

            builder.Property(c => c.TotalWeight)
                   .HasConversion(c => c.Value, c => new Weight(c))
                   .HasColumnName("TotalWeight");

            builder.HasMany(c => c.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(c => c.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(c => c.OrderShippingMethod, mv =>
            {                
                mv.Property(c => c.Name).IsRequired().HasColumnName("ShoppingProvider");
                mv.OwnsOne(c => c.Price, mv =>
                {
                    mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("ShippingPriceCurrency");
                    mv.Property(p => p.Amount).HasColumnName("ShippingPriceAmount").HasPrecision(18, 2);
                });
            });            
            
            builder.ToTable("Orders");
        }
    }
}
