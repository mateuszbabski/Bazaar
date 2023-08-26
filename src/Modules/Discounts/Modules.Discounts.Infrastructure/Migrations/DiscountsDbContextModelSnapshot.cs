﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Discounts.Infrastructure.Context;

#nullable disable

namespace Modules.Discounts.Infrastructure.Migrations
{
    [DbContext(typeof(DiscountsDbContext))]
    partial class DiscountsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modules.Discounts.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("Currency");

                    b.Property<decimal>("DiscountValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("DiscountValue");

                    b.Property<bool>("IsPercentageDiscount")
                        .HasColumnType("bit")
                        .HasColumnName("IsPercentageDiscount");

                    b.HasKey("Id");

                    b.ToTable("Discounts", (string)null);
                });

            modelBuilder.Entity("Modules.Discounts.Domain.Entities.DiscountCoupon", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiscountCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("ExpirationDate");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("StartsAt")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("StartsAt");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.ToTable("DiscountCoupons", (string)null);
                });

            modelBuilder.Entity("Modules.Discounts.Domain.Entities.Discount", b =>
                {
                    b.OwnsOne("Modules.Discounts.Domain.Entities.DiscountTarget", "DiscountTarget", b1 =>
                        {
                            b1.Property<Guid>("DiscountId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("DiscountType")
                                .HasColumnType("int")
                                .HasColumnName("DiscountType");

                            b1.Property<Guid?>("TargetId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("TargetId");

                            b1.HasKey("DiscountId");

                            b1.ToTable("Discounts");

                            b1.WithOwner()
                                .HasForeignKey("DiscountId");
                        });

                    b.Navigation("DiscountTarget")
                        .IsRequired();
                });

            modelBuilder.Entity("Modules.Discounts.Domain.Entities.DiscountCoupon", b =>
                {
                    b.HasOne("Modules.Discounts.Domain.Entities.Discount", "Discount")
                        .WithMany("DiscountCoupons")
                        .HasForeignKey("DiscountId");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Modules.Discounts.Domain.Entities.Discount", b =>
                {
                    b.Navigation("DiscountCoupons");
                });
#pragma warning restore 612, 618
        }
    }
}
