﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Products.Infrastructure.Context;

#nullable disable

namespace Modules.Products.Infrastructure.Migrations
{
    [DbContext(typeof(ProductsDbContext))]
    partial class ProductsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modules.Products.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("WeightPerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Modules.Products.Domain.Entities.Product", b =>
                {
                    b.OwnsOne("Modules.Products.Domain.ValueObjects.ProductCategory", "ProductCategory", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CategoryName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ProductCategory");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Price");

                    b.Navigation("ProductCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
