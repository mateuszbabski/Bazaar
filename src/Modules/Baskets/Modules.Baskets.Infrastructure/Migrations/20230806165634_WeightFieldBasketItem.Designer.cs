﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Baskets.Infrastructure.Context;

#nullable disable

namespace Modules.Baskets.Infrastructure.Migrations
{
    [DbContext(typeof(BasketsDbContext))]
    [Migration("20230806165634_WeightFieldBasketItem")]
    partial class WeightFieldBasketItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modules.Baskets.Domain.Entities.Basket", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("TotalWeight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Baskets", (string)null);
                });

            modelBuilder.Entity("Modules.Baskets.Domain.Entities.BasketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BasketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("BasketItemWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.ToTable("BasketItems", (string)null);
                });

            modelBuilder.Entity("Modules.Baskets.Domain.Entities.Basket", b =>
                {
                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("BasketId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("BasketId");

                            b1.ToTable("Baskets");

                            b1.WithOwner()
                                .HasForeignKey("BasketId");
                        });

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("Modules.Baskets.Domain.Entities.BasketItem", b =>
                {
                    b.HasOne("Modules.Baskets.Domain.Entities.Basket", "Basket")
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "BaseCurrencyPrice", b1 =>
                        {
                            b1.Property<Guid>("BasketItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("BaseAmount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("BaseCurrency");

                            b1.HasKey("BasketItemId");

                            b1.ToTable("BasketItems");

                            b1.WithOwner()
                                .HasForeignKey("BasketItemId");
                        });

                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("BasketItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("BasketItemId");

                            b1.ToTable("BasketItems");

                            b1.WithOwner()
                                .HasForeignKey("BasketItemId");
                        });

                    b.Navigation("BaseCurrencyPrice");

                    b.Navigation("Basket");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("Modules.Baskets.Domain.Entities.Basket", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
