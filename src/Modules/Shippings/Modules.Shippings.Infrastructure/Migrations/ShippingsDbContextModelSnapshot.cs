﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Shippings.Infrastructure.Context.Shippings;

#nullable disable

namespace Modules.Shippings.Infrastructure.Migrations
{
    [DbContext(typeof(ShippingsDbContext))]
    partial class ShippingsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modules.Shippings.Domain.Entities.Shipping", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTimeOffset>("LastUpdateDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShippingMethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("ShippingStatus");

                    b.Property<decimal?>("TotalWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("TrackingNumber")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Shippings", (string)null);
                });

            modelBuilder.Entity("Modules.Shippings.Domain.Entities.Shipping", b =>
                {
                    b.OwnsOne("Shared.Domain.ValueObjects.MoneyValue", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("ShippingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Currency");

                            b1.HasKey("ShippingId");

                            b1.ToTable("Shippings");

                            b1.WithOwner()
                                .HasForeignKey("ShippingId");
                        });

                    b.Navigation("TotalPrice");
                });
#pragma warning restore 612, 618
        }
    }
}
