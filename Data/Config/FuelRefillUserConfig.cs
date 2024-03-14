using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Models.Configurations
{
    public class FuelRefillUserConfig : IEntityTypeConfiguration<FuelRefill>
    {
        public void Configure(EntityTypeBuilder<FuelRefill> builder)
        {
            builder.HasKey(fr => fr.FuelRefillId);

            // Add other configurations for FuelRefill entity properties if needed

            // Relationship with FuelRefillUser
            builder.HasMany(fr => fr.FuelRefillUsers)
                .WithOne(fru => fru.FuelRefill)
                .HasForeignKey(fru => fru.FuelRefillId);
        }
    }
}
