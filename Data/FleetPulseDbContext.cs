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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleModelConfig());

            modelBuilder.ApplyConfiguration(new VehicleTypeConfig());

            modelBuilder.ApplyConfiguration(new ManufactureConfig());
        }
    }
}
