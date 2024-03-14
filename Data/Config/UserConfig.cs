using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users"); // Set the table name
            
            // Set primary key
            builder.HasKey(u => u.UserId);

            // Configure properties
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.NIC).HasMaxLength(12);
            builder.Property(u => u.DriverLicenseNo).HasMaxLength(20);
            builder.Property(u => u.LicenseExpiryDate).IsRequired();
            builder.Property(u => u.BloodGroup).HasMaxLength(5);
            builder.Property(u => u.DateOfBirth).IsRequired();
            builder.Property(u => u.PhoneNo).HasMaxLength(15);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(100); // Hashed passwords should be stored
            builder.Property(u => u.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Property(u => u.EmergencyContact).HasMaxLength(15);
            builder.Property(u => u.JobTitle).HasMaxLength(100);
            builder.Property(u => u.ProfilePicture).HasColumnType("varbinary(max)"); // Adjust type as needed
            builder.Property(u => u.Status).IsRequired();

            // Configure relationships
            builder.HasMany(u => u.TripUsers)
                   .WithOne(tu => tu.User)
                   .HasForeignKey(tu => tu.UserId);

            builder.HasMany(u => u.FuelRefillUsers)
                   .WithOne(fru => fru.User)
                   .HasForeignKey(fru => fru.UserId);

            builder.HasMany(u => u.AccidentUsers)
                   .WithOne(au => au.User)
                   .HasForeignKey(au => au.UserId);
        }
    }
}
