﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProductInStorageApp.DB;

#nullable disable

namespace ProductsInStoragesApp.Migrations
{
    [DbContext(typeof(ProductInStorageContext))]
    partial class ProductInStorageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProductInStorageApp.Model.ProductInStorage", b =>
                {
                    b.Property<int?>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("ProductId"));

                    b.Property<int?>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<int?>("StorageId")
                        .HasColumnType("integer")
                        .HasColumnName("storage_id");

                    b.HasKey("ProductId")
                        .HasName("product_pkey");

                    b.ToTable("storages", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
