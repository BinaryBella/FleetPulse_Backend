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
        private readonly IVehicleMaintenanceConfigurationService _vehicleMaintenanceConfigurationService;
        private readonly IConfiguration _configuration;

        public PushNotificationService(
            FleetPulseDbContext context,
            ILogger<PushNotificationService> logger,
            IVehicleMaintenanceConfigurationService vehicleMaintenanceConfigurationService,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _vehicleMaintenanceConfigurationService = vehicleMaintenanceConfigurationService;
            _configuration = configuration;

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("Configuration/serviceAccountKey.json"),
                });
            }
        }

        public async Task SendNotificationAsync(string fcmDeviceToken, string title, string message, int userId)
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
                }
            };

            try
            {
                var response = await FirebaseMessaging.DefaultInstance.SendAsync(notification);
                _logger.LogInformation("Successfully sent message: " + response);

                // Save notification to the database
                var dbNotification = new FCMNotification
                {
                    Title = title,
                    Message = message,
                    UserName = await GetUserNameByIdAsync(userId),
                    Status = false
                };
                await SaveNotificationAsync(dbNotification);
            }
            catch (FirebaseMessagingException ex)
            {
                if (ex.MessagingErrorCode == MessagingErrorCode.Unregistered ||
                    ex.MessagingErrorCode == MessagingErrorCode.InvalidArgument)
                {
                    _logger.LogError(ex, $"Invalid or unregistered FCM Device Token: {fcmDeviceToken}");
                }
                else
                {
                    _logger.LogError(ex, "Error sending notification.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification.");
            }
        }

        private async Task<string> GetUserNameByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.UserName ?? "Unknown";
        }

        public async Task SendMaintenanceNotificationAsync()
        {
            var dueTasks = await _vehicleMaintenanceConfigurationService.GetDueMaintenanceTasksAsync();

            if (dueTasks == null || dueTasks.Count == 0)
            {
                _logger.LogInformation("No maintenance tasks are due.");
                return;
            }

            var deviceTokens = _configuration.GetSection("DeviceTokens").Get<List<string>>();

            foreach (var task in dueTasks)
            {
                var message = $"Vehicle {task.VehicleId} requires maintenance for {task.TypeName}.";

                foreach (var token in deviceTokens)
                {
                    await SendNotificationAsync(token, "Maintenance Due", message, 0);
                }
            }
        }

        public async Task SaveNotificationAsync(FCMNotification notification)
        {
            try
            {
                notification.NotificationId = Guid.NewGuid().ToString();
                notification.Date = DateTime.UtcNow;
                notification.Time = DateTime.UtcNow.TimeOfDay;

                _context.FCMNotifications.Add(notification); // Ensure you're adding the notification to the context
                await _context.SaveChangesAsync(); // Save changes to persist the notification

                _logger.LogInformation("Notification saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving notification.");
                throw; // Ensure any exceptions are properly handled or logged
            }
        }

        public async Task<List<FCMNotification>> GetAllNotificationsAsync()
        {
            try
            {
                return await _context.FCMNotifications.ToListAsync();
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
                var notification = await _context.FCMNotifications.FindAsync(notificationId);
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
                var notifications = await _context.FCMNotifications.ToListAsync();
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
                var notification = await _context.FCMNotifications.FindAsync(notificationId);
                if (notification != null)
                {
                    _context.FCMNotifications.Remove(notification);
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
                var notifications = await _context.FCMNotifications.ToListAsync();
                _context.FCMNotifications.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all notifications.");
            }
        }
    }
}