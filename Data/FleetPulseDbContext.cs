using FleetPulse_BackEndDevelopment.Data;
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
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<FuelRefill> FuelRefills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany()
                .HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleModel)
                .WithMany()
                .HasForeignKey(v => v.VehicleModelId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Manufacture)
                .WithMany()
                .HasForeignKey(v => v.ManufactureId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.FuelRefill)
                .WithMany()
                .HasForeignKey(v => v.FuelRefillId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
