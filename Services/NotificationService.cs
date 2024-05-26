using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using FleetPulse_BackEndDevelopment.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FleetPulseDbContext _context;

        public NotificationService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task CreateNotificationAsync(string userId, string type, string title, string message)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = userId,
                Title = title,
                NotificationType = type,
                Message = message,
                Date = DateTime.UtcNow.Date,
                Time = DateTime.UtcNow,
                Status = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NotificationDTO>> GetUserNotificationsAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.Status)
                .OrderByDescending(n => n.Date)
                .ThenByDescending(n => n.Time)
                .Select(n => new NotificationDTO
                {
                    NotificationId = n.NotificationId,
                    UserId = n.UserId,
                    Title = n.Title,
                    NotificationType = n.NotificationType,
                    Message = n.Message,
                    Date = n.Date,
                    Time = n.Time,
                    Status = n.Status
                })
                .ToListAsync();
        }

        public async Task MarkNotificationAsReadAsync(string notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.Status = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
