using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace FleetPulse_BackEndDevelopment.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly FleetPulseDbContext _context;
        private readonly ILogger<PushNotificationService> _logger;

        public PushNotificationService(FleetPulseDbContext context, ILogger<PushNotificationService> logger)
        {
            _context = context;
            _logger = logger;
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("config/serviceAccountKey.json"),
                });
            }
        }

        public async Task SendNotificationAsync(string fcmDeviceToken, string title, string message, string username)
        {
            if (string.IsNullOrEmpty(fcmDeviceToken))
            {
                _logger.LogWarning("FCM Device Token not found.");
                return;
            }

            var notification = new Message()
            {
                Token = fcmDeviceToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = message
                },
                Data = new Dictionary<string, string>
                {
                    { "username", username }
                }
            };

            try
            {
                var response = await FirebaseMessaging.DefaultInstance.SendAsync(notification);
                _logger.LogInformation("Successfully sent message: " + response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification.");
            }
        }

        public async Task SaveNotificationAsync(FCMNotification notification)
        {
            try
            {
                notification.NotificationId = Guid.NewGuid().ToString();
                notification.Date = DateTime.UtcNow;
                notification.Time = DateTime.UtcNow.TimeOfDay;
                await _context.FCMNotification.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving notification.");
            }
        }

        public async Task<List<FCMNotification>> GetAllNotificationsAsync()
        {
            try
            {
                return await _context.FCMNotification.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notifications.");
                return new List<FCMNotification>();
            }
        }

        public async Task MarkNotificationAsReadAsync(string notificationId)
        {
            try
            {
                var notification = await _context.FCMNotification.FindAsync(notificationId);
                if (notification != null)
                {
                    notification.Status = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read.");
            }
        }

        public async Task MarkAllAsReadAsync()
        {
            try
            {
                var notifications = await _context.FCMNotification.ToListAsync();
                notifications.ForEach(n => n.Status = true);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read.");
            }
        }

        public async Task DeleteNotificationAsync(string notificationId)
        {
            try
            {
                var notification = await _context.FCMNotification.FindAsync(notificationId);
                if (notification != null)
                {
                    _context.FCMNotification.Remove(notification);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting notification.");
            }
        }

        public async Task DeleteAllNotificationsAsync()
        {
            try
            {
                var notifications = await _context.FCMNotification.ToListAsync();
                _context.FCMNotification.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all notifications.");
            }
        }
    }
}
