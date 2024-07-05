using FleetPulse_BackEndDevelopment.DTOs;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(IPushNotificationService pushNotificationService, ILogger<NotificationController> logger)
        {
            _pushNotificationService = pushNotificationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FCMNotification>>> GetNotifications()
        {
            var notifications = await _pushNotificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }
        
        [HttpGet("unread/{userId}")]
        public async Task<ActionResult<IEnumerable<FCMNotification>>> GetUnreadNotifications(int userId)
        {
            var notifications = await _pushNotificationService.GetUnreadNotificationsAsync(userId);
            return Ok(notifications);
        }
        
        [HttpPost("save-notification")]
        public async Task<IActionResult> SaveNotification([FromBody] FCMNotification notification)
        {
            if (notification == null)
            {
                return BadRequest("Invalid notification data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                notification.NotificationId = Guid.NewGuid().ToString();
        
                if (notification.Date == default)
                {
                    notification.Date = DateTime.UtcNow.Date;
                }

                // Extract only the time part from DateTime
                if (notification.Time == default)
                {
                    notification.Time = DateTime.UtcNow.TimeOfDay;
                }

                await _pushNotificationService.SaveNotificationAsync(notification);
                return Ok(new { Status = "Success", Message = "Notification saved successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the notification");
                return StatusCode(500, new { Status = "Error", Message = "An error occurred while saving the notification.", Detail = ex.Message });
            }
        }


        [HttpPost("mark-as-read/{id}")]
        public async Task<IActionResult> MarkNotificationAsRead(string id)
        {
            await _pushNotificationService.MarkNotificationAsReadAsync(id);
            return Ok(new { Status = "Success", Message = "Notification marked as read" });
        }

        [HttpPost("markAllAsRead")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await _pushNotificationService.MarkAllAsReadAsync();
            return Ok(new { Status = "Success", Message = "All notifications marked as read" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNotification(string id)
        {
            await _pushNotificationService.DeleteNotificationAsync(id);
            return Ok(new { Status = "Success", Message = "Notification deleted successfully" });
        }

        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllNotifications()
        {
            await _pushNotificationService.DeleteAllNotificationsAsync();
            return Ok(new { Status = "Success", Message = "All notifications deleted" });
        }

        [HttpPost("send-reset-password-notification")]
        public async Task<IActionResult> SendResetPasswordNotification([FromBody] FCMNotificationDTO notificationDto)
        {
            if (notificationDto == null || string.IsNullOrEmpty(notificationDto.EmailAddress))
            {
                return BadRequest("Invalid notification data.");
            }

            var notification = new FCMNotificationDTO
            {
                NotificationId = Guid.NewGuid().ToString(),
                Username = notificationDto.Username,
                JobTitle = notificationDto.JobTitle,
                Title = "Password Reset Request",
                Message = "User has requested a password reset.",
                Date = DateTime.UtcNow,
                Time = DateTime.UtcNow.TimeOfDay,
                EmailAddress = notificationDto.EmailAddress,
                Status = false
            };

            var result = await _pushNotificationService.SendNotificationAsynctoAdmin(notification);

            if (result)
            {
                return Ok("Notification sent successfully.");
            }

            return StatusCode(500, "Error sending notification.");
        }
    }
}
