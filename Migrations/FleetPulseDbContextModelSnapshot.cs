﻿// <auto-generated />
using System;
using FleetPulse_BackEndDevelopment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    [DbContext(typeof(FleetPulseDbContext))]
    partial class FleetPulseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Accident", b =>
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FuelRefill", b =>
                {
                    b.Property<int>("FuelRefillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuelRefillId"), 1L, 1);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FType")
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FuelRefillUser", b =>
                {
                    b.Property<int>("FuelRefillId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FuelRefillId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FuelRefillUsers");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Manufacture", b =>
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Trip", b =>
                {
                    b.Property<string>("TripId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("EndMeterValue")
                        .HasColumnType("real");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("StartLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("StartMeterValue")
                        .HasColumnType("real");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("TripId");

                    b.ToTable("Trips", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.TripUser", b =>
                {
                    b.Property<string>("TripId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TripId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TripUsers");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("BloodGroup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverLicenseNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LicenseExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"), 1L, 1);

                    b.Property<string>("AccidentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FuelRefillId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LicenseExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicenseNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ManufactureId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TripId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VehicleColor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VehicleMaintenanceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VehicleModelId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleRegistrationNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("AccidentId");

                    b.HasIndex("FuelRefillId");

                    b.HasIndex("ManufactureId");

                    b.HasIndex("TripId");

                    b.HasIndex("VehicleMaintenanceId");

                    b.HasIndex("VehicleModelId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenance", b =>
                {
                    b.Property<string>("VehicleMaintenanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("MaintenanceDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("MaintenanceStatus")
                        .HasColumnType("bit");

                    b.Property<string>("PartsReplaced")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ServiceProvider")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SpecialNotes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("VehicleMaintenanceId");

                    b.HasIndex("Id");

                    b.ToTable("VehicleMaintenances", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceType", b =>
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleModel", b =>
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleType", b =>
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FuelRefillUser", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.FuelRefill", "FuelRefill")
                        .WithMany("FuelRefillUsers")
                        .HasForeignKey("FuelRefillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.User", "User")
                        .WithMany("FuelRefillUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelRefill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.TripUser", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Trip", "Trip")
                        .WithMany("TripUsers")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.User", "User")
                        .WithMany("TripUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Vehicle", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Accident", "Accident")
                        .WithMany("Vehicles")
                        .HasForeignKey("AccidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.FuelRefill", "FType")
                        .WithMany("Vehicles")
                        .HasForeignKey("FuelRefillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Manufacture", "Manufacturer")
                        .WithMany("Vehicles")
                        .HasForeignKey("ManufactureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Trip", "Trip")
                        .WithMany("Vehicles")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.VehicleMaintenance", "VehicleMaintenance")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleMaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.VehicleModel", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.VehicleType", "Type")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accident");

                    b.Navigation("FType");

                    b.Navigation("Manufacturer");

                    b.Navigation("Model");

                    b.Navigation("Trip");

                    b.Navigation("Type");

                    b.Navigation("VehicleMaintenance");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenance", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceType", "TypeName")
                        .WithMany("VehicleMaintenances")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeName");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Accident", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FuelRefill", b =>
                {
                    b.Navigation("FuelRefillUsers");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Manufacture", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Trip", b =>
                {
                    b.Navigation("TripUsers");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.User", b =>
                {
                    b.Navigation("FuelRefillUsers");

                    b.Navigation("TripUsers");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenance", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceType", b =>
                {
                    b.Navigation("VehicleMaintenances");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleModel", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
