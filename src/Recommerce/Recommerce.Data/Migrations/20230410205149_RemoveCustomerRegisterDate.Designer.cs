﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recommerce.Data.DbContexts;

#nullable disable

namespace Recommerce.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230410205149_RemoveCustomerRegisterDate")]
    partial class RemoveCustomerRegisterDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Recommerce.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("DateTime");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<byte?>("GenderType")
                        .HasColumnType("TinyInt");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastLoginDate")
                        .IsRequired()
                        .HasColumnType("DateTime");

                    b.Property<int?>("ShoppingBalance")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("UniqueIdentifier")
                        .IsRequired()
                        .HasColumnType("varChar(50)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerLocations");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("DeviceBrand")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DeviceOs")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("UniqueIdentifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueIdentifier")
                        .HasDefaultValueSql("NewId()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerSessions");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerSessionProductMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CustomerSessionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerSessionId");

                    b.HasIndex("ProductId");

                    b.ToTable("CustomerSessionProductMappings");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerWishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("CustomerWishLists");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerLocationId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerSessionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("OrderUniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UniquePrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerLocationId");

                    b.HasIndex("CustomerSessionId");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CommentCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double?>("ReviewRate")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UniqueIdentifier")
                        .IsRequired()
                        .HasColumnType("varChar(50)");

                    b.Property<int?>("WeightInKg")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.ProductCategoryMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategoryMappings");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.ProductReviewMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("varChar(1000)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmotionType")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double?>("Rate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductReviewMappings");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerLocation", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.Customer", "Customer")
                        .WithMany("CustomerLocations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerSession", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.Customer", "Customer")
                        .WithMany("CustomerSessions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerSessionProductMapping", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.CustomerSession", "CustomerSession")
                        .WithMany("CustomerSessionProductMappings")
                        .HasForeignKey("CustomerSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recommerce.Data.Entities.Product", "Product")
                        .WithMany("CustomerSessionProductMappings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerSession");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerWishList", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.Customer", "Customer")
                        .WithMany("CustomerWishLists")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recommerce.Data.Entities.Product", "Product")
                        .WithMany("CustomerWishLists")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.Order", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recommerce.Data.Entities.CustomerLocation", "CustomerLocation")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerLocationId");

                    b.HasOne("Recommerce.Data.Entities.CustomerSession", "CustomerSession")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerSessionId");

                    b.HasOne("Recommerce.Data.Entities.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("CustomerLocation");

                    b.Navigation("CustomerSession");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.ProductCategoryMapping", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.Product", "Product")
                        .WithMany("ProductCategoryMappings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.ProductReviewMapping", b =>
                {
                    b.HasOne("Recommerce.Data.Entities.Customer", "Customer")
                        .WithMany("ProductReviewMappings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recommerce.Data.Entities.Product", "Product")
                        .WithMany("ProductReviewMappings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.Customer", b =>
                {
                    b.Navigation("CustomerLocations");

                    b.Navigation("CustomerSessions");

                    b.Navigation("CustomerWishLists");

                    b.Navigation("Orders");

                    b.Navigation("ProductReviewMappings");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerLocation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.CustomerSession", b =>
                {
                    b.Navigation("CustomerSessionProductMappings");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Recommerce.Data.Entities.Product", b =>
                {
                    b.Navigation("CustomerSessionProductMappings");

                    b.Navigation("CustomerWishLists");

                    b.Navigation("Orders");

                    b.Navigation("ProductCategoryMappings");

                    b.Navigation("ProductReviewMappings");
                });
#pragma warning restore 612, 618
        }
    }
}
