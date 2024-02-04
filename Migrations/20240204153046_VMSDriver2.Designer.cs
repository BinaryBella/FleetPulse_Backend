﻿// <auto-generated />
using System;
using FleetPulse_BackEndDevelopment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    [DbContext(typeof(FleetPulseDbContext))]
    [Migration("20240204153046_VMSDriver2")]
    partial class VMSDriver2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.Accident", b =>
                {
                    b.Property<string>("AccidentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DriverInjuredStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("HelperInjuredStatus")
                        .HasColumnType("bit");

                    b.Property<decimal>("Loss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("Photos")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varbinary(500)");

                    b.Property<string>("SpecialNotes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("VehicleDamagedStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AccidentId");

                    b.ToTable("Accidents", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriverId"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverLicenseNo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EmergencyContact")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LicenseExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varbinary(1000)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.FuelRefill", b =>
                {
                    b.Property<int>("FuelRefillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuelRefillId"), 1L, 1);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("LiterCount")
                        .HasColumnType("float");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("FuelRefillId");

                    b.ToTable("FuelRefills", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.Manufacture", b =>
                {
                    b.Property<int>("ManufactureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManufactureId"), 1L, 1);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ManufactureId");

                    b.ToTable("Manufacture", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.VehicleMaintenanceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("VehicleMaintenanceTypes", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.VehicleModel", b =>
                {
                    b.Property<int>("VehicleModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleModelId"), 1L, 1);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("VehicleModelId");

                    b.ToTable("VehicleModel", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.VehicleType", b =>
                {
                    b.Property<int>("VehicleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleTypeId"), 1L, 1);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("VehicleTypeId");

                    b.ToTable("VehicleType", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
