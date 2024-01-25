using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class VehicleModelConfig : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.ToTable("VehicleModel");

            builder.HasKey(x => x.VehicleModelId);

            builder.Property(x => x.VehicleModelId).UseIdentityColumn();
            builder.Property(n => n.VehicleModelId).IsRequired();
            builder.Property(n => n.Model).IsRequired().HasMaxLength(250);
        }
    }
}