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
    [Migration("20240701100306_ModifyTripModel")]
    partial class ModifyTripModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Accident", b =>
                {
                    b.Property<int>("AccidentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccidentId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<bool>("DriverInjuredStatus")
                        .HasColumnType("bit");

                    b.Property<int>("HelperId")
                        .HasColumnType("int");

                    b.Property<bool>("HelperInjuredStatus")
                        .HasColumnType("bit");

                    b.Property<decimal>("Loss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LossStatement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<bool>("VehicleDamagedStatus")
                        .HasColumnType("bit");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AccidentId");

                    b.ToTable("Accidents", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.AccidentUser", b =>
                {
                    b.Property<int>("AccidentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AccidentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AccidentUsers");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.DeviceToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceTokens");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FleetPulse_BackEndDevelopment.Models.FCMNotification", b =>
                {
                    b.Property<string>("NotificationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NotificationId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FuelRefillUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FuelRefillId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FuelRefillId");

                    b.HasIndex("FuelRefillId");

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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Trip", b =>
                {
                    b.Property<string>("TripId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("EndMeterValue")
                        .HasColumnType("real");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

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

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TripUserId")
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
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverLicenseNo")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmergencyContact")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("LicenseExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"), 1L, 1);

                    b.Property<int?>("AccidentId")
                        .HasColumnType("int");

                    b.Property<int?>("FuelRefillId")
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TripId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VehicleColor")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenance", b =>
                {
                    b.Property<int>("MaintenanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaintenanceId"), 1L, 1);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("MaintenanceDate")
                        .HasColumnType("datetime2");

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

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleMaintenanceTypeId")
                        .HasColumnType("int");

                    b.HasKey("MaintenanceId");

                    b.HasIndex("VehicleId");

                    b.HasIndex("VehicleMaintenanceTypeId");

                    b.ToTable("VehicleMaintenances", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleMaintenanceTypeId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleRegistrationNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VehicleMaintenanceConfigurations");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("VehicleMaintenanceTypes", (string)null);
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

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VerificationCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("ExpirationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("VerificationCodes", (string)null);
                });

            modelBuilder.Entity("FuelRefill", b =>
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("FuelRefillId");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("FuelRefills", (string)null);
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.AccidentUser", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Accident", "Accident")
                        .WithMany("AccidentUsers")
                        .HasForeignKey("AccidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.User", "User")
                        .WithMany("AccidentUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accident");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.FuelRefillUser", b =>
                {
                    b.HasOne("FuelRefill", "FuelRefill")
                        .WithMany("FuelRefillUsers")
                        .HasForeignKey("FuelRefillId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.User", "User")
                        .WithMany("FuelRefillUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FuelRefill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.RefreshToken", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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
                        .HasForeignKey("AccidentId");

                    b.HasOne("FuelRefill", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("FuelRefillId");

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Manufacture", "Manufacturer")
                        .WithMany("Vehicles")
                        .HasForeignKey("ManufactureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Trip", "Trip")
                        .WithMany("Vehicles")
                        .HasForeignKey("TripId");

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.VehicleType", "Type")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accident");

                    b.Navigation("Manufacturer");

                    b.Navigation("Trip");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenance", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Vehicle", "Vehicle")
                        .WithMany("VehicleMaintenances")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceType", "VehicleMaintenanceType")
                        .WithMany("VehicleMaintenances")
                        .HasForeignKey("VehicleMaintenanceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");

                    b.Navigation("VehicleMaintenanceType");
                });

            modelBuilder.Entity("FuelRefill", b =>
                {
                    b.HasOne("FleetPulse_BackEndDevelopment.Models.User", "User")
                        .WithMany("FuelRefills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetPulse_BackEndDevelopment.Models.Vehicle", "Vehicle")
                        .WithMany("FuelRefills")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Accident", b =>
                {
                    b.Navigation("AccidentUsers");

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
                    b.Navigation("AccidentUsers");

                    b.Navigation("FuelRefillUsers");

                    b.Navigation("FuelRefills");

                    b.Navigation("RefreshTokens");

                    b.Navigation("TripUsers");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.Vehicle", b =>
                {
                    b.Navigation("FuelRefills");

                    b.Navigation("VehicleMaintenances");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleMaintenanceType", b =>
                {
                    b.Navigation("VehicleMaintenances");
                });

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Models.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FuelRefill", b =>
                {
                    b.Navigation("FuelRefillUsers");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
