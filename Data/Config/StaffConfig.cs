using FleetPulse_BackEndDevelopment.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class StaffConfig : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");

            builder.HasKey(s => s.StaffId);

            builder.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.LastName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.NIC).IsRequired().HasMaxLength(20);
            builder.Property(s => s.DateOfBirth).IsRequired();
            builder.Property(s => s.PhoneNo).IsRequired().HasMaxLength(20);
            builder.Property(s => s.UserName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Password).IsRequired().HasMaxLength(100);
            builder.Ignore(s => s.ConfirmPassword);
            builder.Property(s => s.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Property(s => s.JobTitle).HasMaxLength(100);
            builder.Property(s => s.ProfilePicture).HasMaxLength(500);
            builder.Property(s => s.Status).IsRequired();
        }
    }
}

