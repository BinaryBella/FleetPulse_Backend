using FleetPulse_BackEndDevelopment.Data.Config;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class FleetPulseDbContext : DbContext
    {
        public FleetPulseDbContext(DbContextOptions<FleetPulseDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<FCMNotification> FCMNotifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<FuelRefill> FuelRefills { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public DbSet<VehicleMaintenanceType> VehicleMaintenanceTypes { get; set; }
        public DbSet<VehicleMaintenanceConfiguration> VehicleMaintenanceConfigurations { get; set; }
        public DbSet<TripUser> TripUsers { get; set; }
        public DbSet<AccidentUser> AccidentUsers { get; set; }
        public DbSet<FuelRefillUser> FuelRefillUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VehicleTypeConfig());
            modelBuilder.ApplyConfiguration(new ManufactureConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new AccidentConfig());
            modelBuilder.ApplyConfiguration(new AccidentUserConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceTypeConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfig());
            modelBuilder.ApplyConfiguration(new FCMNotificationConfig());

            // One-to-many relationships
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Trips)
                .WithOne(t => t.Vehicle)
                .HasForeignKey(t => t.VehicleId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.FuelRefills)
                .WithOne(f => f.Vehicle)
                .HasForeignKey(f => f.VehicleId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Accidents)
                .WithOne(a => a.Vehicle)
                .HasForeignKey(a => a.VehicleId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.VehicleMaintenances)
                .WithOne(vm => vm.Vehicle)
                .HasForeignKey(vm => vm.VehicleId);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.TripUsers)
                .WithOne(tu => tu.Trip)
                .HasForeignKey(tu => tu.TripId);

            modelBuilder.Entity<FuelRefill>()
                .HasMany(fr => fr.FuelRefillUsers)
                .WithOne(fru => fru.FuelRefill)
                .HasForeignKey(fru => fru.FuelRefillId);

            modelBuilder.Entity<Accident>()
                .HasMany(a => a.AccidentUsers)
                .WithOne(au => au.Accident)
                .HasForeignKey(au => au.AccidentId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Type)
                .WithMany()
                .HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Manufacturer)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(v => v.ManufactureId);

            modelBuilder.Entity<FuelRefill>()
                .HasOne(fr => fr.User)
                .WithMany()
                .HasForeignKey(fr => fr.UserId);

            //many to many
            modelBuilder.Entity<TripUser>()
                .HasKey(tu => new { tu.TripId, tu.UserId });

            modelBuilder.Entity<TripUser>()
                .HasOne(tu => tu.Trip)
                .WithMany(t => t.TripUsers)
                .HasForeignKey(tu => tu.TripId);

            modelBuilder.Entity<TripUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TripUsers)
                .HasForeignKey(tu => tu.UserId);

            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne(vm => vm.Vehicle)
                .WithMany(v => v.VehicleMaintenances)
                .HasForeignKey(vm => vm.VehicleId);

            modelBuilder.Entity<AccidentUser>()
                .HasOne(au => au.User)
                .WithMany(u => u.AccidentUsers)
                .HasForeignKey(au => au.UserId);

            modelBuilder.Entity<AccidentUser>().HasKey(au => new { au.AccidentId, au.UserId });

            modelBuilder.Entity<AccidentUser>()
                .HasOne(au => au.Accident)
                .WithMany(a => a.AccidentUsers)
                .HasForeignKey(au => au.AccidentId);

            modelBuilder.Entity<FuelRefillUser>()
                .HasKey(fr => new { fr.UserId, fr.FuelRefillId });

            modelBuilder.Entity<FuelRefillUser>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FuelRefillUsers)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FuelRefillUser>()
                .HasOne(fr => fr.FuelRefill)
                .WithMany(f => f.FuelRefillUsers)
                .HasForeignKey(fr => fr.FuelRefillId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
