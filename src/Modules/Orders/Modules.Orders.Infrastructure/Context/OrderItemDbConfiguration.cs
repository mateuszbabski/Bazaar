using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Infrastructure.Context
{
    internal sealed class OrderItemDbConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasConversion(c => c.Value, c => new OrderItemId(c));

            builder.Property(c => c.OrderId)
                   .HasConversion(c => c.Value, c => new OrderId(c));

            builder.HasOne(c => c.Order)
                   .WithMany(p => p.Items)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(c => c.Price, mv =>
            {
                mv.Property(p => p.Currency).HasMaxLength(3).HasColumnName("Price");
                mv.Property(p => p.Amount).HasPrecision(18, 2).HasColumnName("Amount");
            });

            builder.Property(c => c.ProductId).HasColumnName("ProductId").IsRequired();

            builder.Property(c => c.ShopId).HasColumnName("ShopId").IsRequired();

            builder.Property(c => c.Quantity).HasColumnName("Quantity");

            builder.ToTable("OrderItems");
        }
    }
}
