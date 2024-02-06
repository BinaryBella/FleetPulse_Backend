using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Models.Configurations
{
    public class MaintenanceUserConfig : IEntityTypeConfiguration<MaintenanceUser>
    {
        public void Configure(EntityTypeBuilder<MaintenanceUser> builder)
        {
            builder.HasKey(mu => new { mu.VehicleMaintenanceId, mu.UserId });

            builder.HasOne<User>(mu => mu.User)
                .WithMany(u => u.MaintenanceUsers)
                .HasForeignKey(mu => mu.UserId);

            builder.HasOne<VehicleMaintenance>(mu => mu.VehicleMaintenance)
                .WithMany(mu => mu.MaintenanceUsers)
                .HasForeignKey(mu => mu.VehicleMaintenanceId);
        }
    }
}
