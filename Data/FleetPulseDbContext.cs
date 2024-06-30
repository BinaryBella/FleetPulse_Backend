using FleetPulse_BackEndDevelopment.Data.Config;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Models.FleetPulse_BackEndDevelopment.Models;
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
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Manufacture> Manufacture { get; set; }
        public DbSet<Accident> Accident { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenance { get; set; }
        public DbSet<VehicleMaintenanceType> VehicleMaintenanceType { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<FCMNotification> FCMNotification { get; set; }
        public DbSet<VehicleMaintenanceConfiguration> VehicleMaintenanceConfigurations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DeviceToken> DeviceTokens { get; set; }
        public DbSet<AccidentUser> AccidentUser { get; internal set; }
        public object VehicleTypes { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccidentUser>()
            .HasKey(e => new { e.UserId, e.AccidentId });

            modelBuilder.Entity<FuelRefillUser>()
            .HasKey(e => new { e.FuelRefillId, e.UserId });

            modelBuilder.Entity<TripUser>()
            .HasKey(e => new { e.UserId, e.TripId });

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
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfig());
            modelBuilder.ApplyConfiguration(new FCMNotificationConfig());

            // configures one-to-many relationship

            //Vehicle_Type
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Model)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleModelId);

            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleType>(v => v.Type)
                .WithMany(vt => vt.Vehicles)
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
                .HasKey(u => new { u.UserId });

            //Many To Many Relationship

            //TripUser
            modelBuilder.Entity<TripUser>().HasKey(tu => new { tu.TripId, tu.UserId });

            modelBuilder.Entity<TripUser>()
                .HasOne<Trip>(tu => tu.Trip)
                .WithMany(v => v.TripUsers)
                .HasForeignKey(tu => tu.TripId);

            modelBuilder.Entity<TripUser>()
                .HasOne<User>(tu => tu.User)
                .WithMany(v => v.TripUsers)
                .HasForeignKey(tu => tu.UserId);

            //FuelRefillUser
            modelBuilder.Entity<FuelRefillUser>().HasKey(fru => new { fru.FuelRefillId, fru.UserId });

            modelBuilder.Entity<FuelRefillUser>()
                .HasOne<FuelRefill>(fru => fru.FuelRefill)
                .WithMany(f => f.FuelRefillUsers)
                .HasForeignKey(fru => fru.FuelRefillId);

            modelBuilder.Entity<FuelRefillUser>()
                .HasOne<User>(fru => fru.User)
                .WithMany(f => f.FuelRefillUsers)
                .HasForeignKey(fru => fru.UserId);


            //AccidentUser
            modelBuilder.Entity<AccidentUser>().HasKey(fru => new { fru.AccidentId, fru.UserId });

            modelBuilder.Entity<AccidentUser>()
                .HasOne<Accident>(fru => fru.Accident)
                .WithMany(f => f.AccidentUsers)
                .HasForeignKey(fru => fru.AccidentId);

            modelBuilder.Entity<AccidentUser>()
                .HasOne<User>(fru => fru.User)
                .WithMany(f => f.AccidentUsers)
                .HasForeignKey(fru => fru.UserId);
           
            modelBuilder.Entity<FuelRefill>()
                .HasOne(f => f.User)
                .WithMany(u => u.FuelRefills)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
