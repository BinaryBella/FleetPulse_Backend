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
        
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<Manufacture> Manufacture { get; set; }
        public DbSet<FuelRefill> FuelRefills { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Accident> Accident { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public DbSet<VehicleMaintenanceType> VehicleMaintenanceType { get; set; }
        public DbSet<TripUser> TripUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<FCMNotification> FCMNotification { get; set; }
        public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new VehicleModelConfig());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfig());
            modelBuilder.ApplyConfiguration(new ManufactureConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new VehicleConfig());
            modelBuilder.ApplyConfiguration(new AccidentConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceConfig());
            modelBuilder.ApplyConfiguration(new VehicleMaintenanceTypeConfig());
            modelBuilder.ApplyConfiguration(new TripUserConfig());
            modelBuilder.ApplyConfiguration(new FuelRefillConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VerificationCodeConfig());
            modelBuilder.ApplyConfiguration(new FCMNotificationConfig());

            // Configures one-to-many relationship

            // Vehicle_Model
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleModel>(v => v.Model)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleModelId);
            
            // Vehicle_Type
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleType>(v => v.Type)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);
            
            // Vehicle_Manufacture
            modelBuilder.Entity<Vehicle>()
                .HasOne<Manufacture>(v => v.Manufacturer)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.ManufactureId);
            
            // Vehicle_Maintenance
            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne(v => v.Vehicle)
                .WithMany(vm => vm.VehicleMaintenance)
                .HasForeignKey(vm => vm.VehicleId);
            
            // Vehicle_Maintenance_Type
            modelBuilder.Entity<VehicleMaintenance>()
                .HasOne<VehicleMaintenanceType>(v => v.VehicleMaintenanceType)
                .WithMany(vmt => vmt.VehicleMaintenances)
                .HasForeignKey(v => v.VehicleMaintenanceTypeId);
            
            // Vehicle_FuelRefill
            modelBuilder.Entity<FuelRefill>()
                .HasOne(v => v.Vehicle)
                .WithMany(fr => fr.FuelRefills)
                .HasForeignKey(v => v.VehicleId);
            
            // User
            modelBuilder.Entity<User>()
                .HasKey(u => new { u.UserId });
            
            // Many-To-Many Relationship

            // TripUser
            modelBuilder.Entity<TripUser>().HasKey(tu => new { tu.TripId, tu.UserId });

            modelBuilder.Entity<TripUser>()
                .HasOne<Trip>(tu => tu.Trip)
                .WithMany(v => v.TripUsers)
                .HasForeignKey(tu => tu.TripId);
            
            modelBuilder.Entity<TripUser>()
                .HasOne<User>(tu => tu.User)
                .WithMany(v => v.TripUsers)
                .HasForeignKey(tu => tu.UserId);
            
            modelBuilder.Entity<FuelRefill>()
                .HasOne(f => f.User)
                .WithMany(u => u.FuelRefills)
                .HasForeignKey(f => f.UserId);
            
            // AccidentUser
            modelBuilder.Entity<AccidentUser>().HasKey(fru => new { fru.AccidentId, fru.UserId });

            modelBuilder.Entity<AccidentUser>()
                .HasOne<Accident>(fru => fru.Accident)
                .WithMany(f => f.AccidentUsers)
                .HasForeignKey(fru => fru.AccidentId);
            
            modelBuilder.Entity<AccidentUser>()
                .HasOne<User>(fru => fru.User)
                .WithMany(f => f.AccidentUsers)
                .HasForeignKey(fru => fru.UserId);
        }
    }
}
