using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Configurations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Specify the table name
            builder.ToTable("RefreshTokens");

            // Specify the primary key
            builder.HasKey(rt => rt.Id);

            // Specify properties
            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(rt => rt.Expires)
                .IsRequired();

            builder.Property(rt => rt.IsRevoked)
                .IsRequired();

            builder.Property(rt => rt.UserId)
                .IsRequired();

            // Define relationships if any (e.g., foreign key constraints)
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}