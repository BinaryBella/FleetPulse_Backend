using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task<List<FCMNotification>> GetAllNotificationsAsync();
        Task MarkNotificationAsReadAsync(string notificationId);
        Task MarkAllAsReadAsync();
        Task DeleteNotificationAsync(string notificationId);
        Task DeleteAllNotificationsAsync();
        Task SendMaintenanceNotificationAsync();
        Task SendNotificationAsync(string fcmDeviceToken, string title, string message, int userId);
    }
}