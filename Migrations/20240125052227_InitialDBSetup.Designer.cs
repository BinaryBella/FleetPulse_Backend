﻿// <auto-generated />
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
    [Migration("20240125052227_InitialDBSetup")]
    partial class InitialDBSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FleetPulse_BackEndDevelopment.Data.VehicleModel", b =>
                {
                    b.Property<int>("VehicleModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleModelId"), 1L, 1);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleModelId");

                    b.ToTable("VehicleModels");
                });
#pragma warning restore 612, 618
        }
    }
}
