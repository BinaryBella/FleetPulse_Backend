using FleetPulse_BackEndDevelopment.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Data.Config
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.Title).IsRequired().HasMaxLength(255); // Adjust max length based on your needs
            builder.Property(n => n.NotificationType).IsRequired().HasMaxLength(50); // Adjust max length based on your needs
            builder.Property(n => n.Message).IsRequired().HasMaxLength(1000); // Adjust max length based on your needs
            builder.Property(n => n.Date).IsRequired();
            builder.Property(n => n.Time).IsRequired();
            builder.Property(n => n.Status).IsRequired();

            builder.ToTable("Notifications");
        }
    }
}