using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task SaveNotificationAsync(FCMNotification notification);
        Task<List<FCMNotification>> GetAllNotificationsAsync();
        Task MarkNotificationAsReadAsync(string notificationId);
        Task MarkAllAsReadAsync();
        Task DeleteNotificationAsync(string notificationId);
        Task DeleteAllNotificationsAsync();
        Task SendNotificationAsync(string adminDeviceToken, string notificationTitle, string notificationMessage, string userUserName);
    }
}