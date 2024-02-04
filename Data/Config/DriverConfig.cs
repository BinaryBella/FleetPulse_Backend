using FleetPulse_BackEndDevelopment.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class DriverConfig : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");

            builder.HasKey(d => d.DriverId);

            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.NIC).IsRequired().HasMaxLength(30);
            builder.Property(d => d.DriverLicenseNo).IsRequired().HasMaxLength(30);
            builder.Property(d => d.LicenseExpiryDate).IsRequired();
            builder.Property(d => d.DateOfBirth).IsRequired();
            builder.Property(d => d.PhoneNo).IsRequired().HasMaxLength(30);
            builder.Property(d => d.UserName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Password).IsRequired().HasMaxLength(200);
            builder.Property(d => d.EmailAddress).IsRequired().HasMaxLength(200);
            builder.Property(d => d.EmergencyContact).HasMaxLength(30);
            builder.Property(d => d.ProfilePicture).HasMaxLength(1000);

            builder.Ignore(d => d.ConfirmPassword);
        }
    }

}
