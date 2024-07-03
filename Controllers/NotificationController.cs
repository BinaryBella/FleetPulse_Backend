using FleetPulse_BackEndDevelopment.DTOs;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;

        public NotificationController(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FCMNotification>>> GetNotifications()
        {
            var notifications = await _pushNotificationService.GetAllNotificationsAsync();
            return Ok(notifications);
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
