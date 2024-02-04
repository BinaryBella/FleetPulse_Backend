using FleetPulse_BackEndDevelopment.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class HelperConfig : IEntityTypeConfiguration<Helper>
    {
        public void Configure(EntityTypeBuilder<Helper> builder)
        {
            builder.ToTable("Helpers");

            builder.HasKey(h => h.HelperId);

            builder.Property(h => h.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(h => h.LastName).IsRequired().HasMaxLength(50);
            builder.Property(h => h.NIC).IsRequired().HasMaxLength(20);
            builder.Property(h => h.DateOfBirth).IsRequired();
            builder.Property(h => h.PhoneNo).IsRequired().HasMaxLength(20);
            builder.Property(h => h.UserName).IsRequired().HasMaxLength(50);
            builder.Property(h => h.Password).IsRequired().HasMaxLength(100);
            builder.Ignore(h => h.ConfirmPassword);
            builder.Property(h => h.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Property(h => h.EmergencyContact).HasMaxLength(20);
            builder.Property(h => h.ProfilePicture).HasMaxLength(500);
            builder.Property(h => h.Status).IsRequired();
        }
    }
}
