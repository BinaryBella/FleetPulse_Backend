using FleetPulse_BackEndDevelopment.DTOs;
using FleetPulse_BackEndDevelopment.Models;
namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string userId, string type, string title, string message);
        Task<List<NotificationDTO>> GetUserNotificationsAsync(string userId);
        Task MarkNotificationAsReadAsync(string notificationId);
    }
}