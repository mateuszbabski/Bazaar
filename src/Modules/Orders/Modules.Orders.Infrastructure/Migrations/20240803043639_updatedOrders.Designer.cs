﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Orders.Infrastructure.Context;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    [DbContext(typeof(OrdersDbContext))]
    [Migration("20240803043639_updatedOrders")]
    partial class updatedOrders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modules.Orders.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTimeOffset>("LastUpdateDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int")
                        .HasColumnName("OrderStatus");

                    b.Property<decimal>("TotalWeight")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalWeight");

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Modules.Orders.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ShopId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("Modules.Orders.Domain.Entities.Order", b =>
                {
                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("TotalPrice");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Modules.Orders.Domain.Entities.OrderShippingMethod", "OrderShippingMethod", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ShoppingMethodProviderId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ShoppingProvider");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "Price", b2 =>
                                {
                                    b2.Property<Guid>("OrderShippingMethodOrderId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(18, 2)
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("ShippingPriceAmount");

                                    b2.Property<string>("Currency")
                                        .HasMaxLength(3)
                                        .HasColumnType("nvarchar(3)")
                                        .HasColumnName("ShippingPriceCurrency");

                                    b2.HasKey("OrderShippingMethodOrderId");

                                    b2.ToTable("Orders");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderShippingMethodOrderId");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.OwnsOne("Modules.Orders.Domain.Entities.Receiver", "Receiver", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverEmail");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ReceiverId");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverLastName");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverName");

                            b1.Property<string>("TelephoneNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ReceiverPhoneNumber");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Shared.Domain.ValueObjects.Address", "Address", b2 =>
                                {
                                    b2.Property<Guid>("ReceiverOrderId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("ReceiverCity");

                                    b2.Property<string>("Country")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("ReceiverCountry");

                                    b2.Property<string>("PostalCode")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("ReceiverPostalCode");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("ReceiverStreet");

                                    b2.HasKey("ReceiverOrderId");

                                    b2.ToTable("Orders");

                                    b2.WithOwner()
                                        .HasForeignKey("ReceiverOrderId");
                                });

                            b1.Navigation("Address")
                                .IsRequired();
                        });

                    b.Navigation("OrderShippingMethod")
                        .IsRequired();

                    b.Navigation("Receiver")
                        .IsRequired();

                    b.Navigation("TotalPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Modules.Orders.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Modules.Orders.Domain.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Price");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("Order");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("Modules.Orders.Domain.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
