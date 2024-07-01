using FleetPulse_BackEndDevelopment.Data.Config;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class FleetPulseDbContext : DbContext
    {
        public FleetPulseDbContext(DbContextOptions<FleetPulseDbContext> options) : base(options)
        {
        }

        public DbSet<FuelRefill> FuelRefills { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public DbSet<VehicleMaintenanceType> VehicleMaintenanceType { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<FCMNotification> FCMNotifications { get; set; }
        public DbSet<VehicleMaintenanceConfiguration> VehicleMaintenanceConfigurations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VehicleModelConfig());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfig());
            modelBuilder.ApplyConfiguration(new ManufactureConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceTypeConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfig());
            modelBuilder.ApplyConfiguration(new FCMNotificationConfig());

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Model)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleModelId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Type)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Manufacturer)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.ManufactureId);

            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne(v => v.Vehicle)
                .WithMany(vm => vm.VehicleMaintenance)
                .HasForeignKey(vm => vm.VehicleId);

            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne(v => v.VehicleMaintenanceType)
                .WithMany(vmt => vmt.VehicleMaintenances)
                .HasForeignKey(v => v.VehicleMaintenanceTypeId);

            modelBuilder.Entity<FuelRefill>()
                .HasOne(v => v.Vehicle)
                .WithMany(fr => fr.FuelRefills)
                .HasForeignKey(v => v.VehicleId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<FuelRefill>()
                .HasOne(f => f.User)
                .WithMany(u => u.FuelRefills)
                .HasForeignKey(f => f.UserId);
        }
    }
}