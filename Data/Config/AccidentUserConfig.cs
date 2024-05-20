using FleetPulse_BackEndDevelopment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Models.Configurations
{
    public class AccidentUserConfig : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.HasKey(fr => fr.AccidentId);


            // Relationship with AccidentUser
            builder.HasMany(fr => fr.AccidentUsers)
                .WithOne(fru => fru.Accident)
                .HasForeignKey(fru => fru.AccidentId);
        }
    }
}
