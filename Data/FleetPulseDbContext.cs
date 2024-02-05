using FleetPulse_BackEndDevelopment.Data;
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
            
            //Vehicle_Model
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleModel>(v => v.Model)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleModelId);  
            //Vehicle_Type
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleType>(v => v.Type)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);  
            //Vehicle_Manufacture
            modelBuilder.Entity<Vehicle>()
                .HasOne<Manufacture>(v => v.Manufacturer)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.ManufactureId);
            //Vehicle_Maintenance_Type
            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne<VehicleMaintenanceType>(v => v.TypeName)
                .WithMany(vmt => vmt.VehicleMaintenances)
                .HasForeignKey(v => v.Id);
            //FuelRefill
            modelBuilder.Entity<Vehicle>()
                .HasOne<FuelRefill>(v => v.FType)
                .WithMany(fr => fr.Vehicles)
                .HasForeignKey(v => v.FuelRefillId);
            //Vehicle_Maintenance
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleMaintenance>(v => v.VehicleMaintenance)
                .WithMany(vmain => vmain.Vehicles)
                .HasForeignKey(v => v.VehicleMaintenanceId);
            
        }
    }
    
}
