﻿// <auto-generated />
using System;
using HouseBuySell.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HouseBuySell.Web.Migrations.HouseLand
{
    [DbContext(typeof(HouseLandContext))]
    [Migration("20240225174709_addtablee")]
    partial class addtablee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HouseBuySell.Models.Entity.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Features")
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageFullPath")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("HouseBuySell.Models.Entity.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ProprtyType")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("PropertyType");
                });

            modelBuilder.Entity("HouseBuySell.Models.Entity.Property", b =>
                {
                    b.HasOne("HouseBuySell.Models.Entity.PropertyType", "PropertyType")
                        .WithMany("Property")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyType");
                });

            modelBuilder.Entity("HouseBuySell.Models.Entity.PropertyType", b =>
                {
                    b.Navigation("Property");
                });
#pragma warning restore 612, 618
        }
    }
}