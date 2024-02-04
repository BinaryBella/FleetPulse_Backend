﻿using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.Config;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class FleetPulseDbContext : DbContext
    {
        public FleetPulseDbContext(DbContextOptions<FleetPulseDbContext> options) : base(options)
        {
        }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Manufacture> Manufacture { get; set; }
        public DbSet<FuelRefill> FuelRefill { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Accident> Accident { get; set; }
        public DbSet<Helper> Helper { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenance { get; set; }
        public DbSet<VehicleMaintenanceType> VehicleMaintenanceType { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleModelConfig());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfig());
            modelBuilder.ApplyConfiguration(new ManufactureConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new DriverConfig());
            modelBuilder.ApplyConfiguration(new AccidentConfig());
            modelBuilder.ApplyConfiguration(new HelperConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceTypeConfig());
            
            // configures one-to-many relationship
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleModel>(v => v.Model)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleModelId);          
        }
    }
}
