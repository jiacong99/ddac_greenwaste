﻿// <auto-generated />
using System;
using DDAC_Assignment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDAC_Assignment.Migrations.DDAC_
{
    [DbContext(typeof(DDAC_Context))]
    [Migration("20210729075518_data_context_v2")]
    partial class data_context_v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDAC_Assignment.Models.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BookingLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BookingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BookingStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookingType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingID");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("DDAC_Assignment.Models.WasteServices", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("serviceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("serviceDescription")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("serviceMediaURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("serviceSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("servicesTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("WasteServices");
                });
#pragma warning restore 612, 618
        }
    }
}
