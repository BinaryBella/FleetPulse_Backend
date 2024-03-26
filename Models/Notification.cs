using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Notification
    {
        [Key]
        public string NotificationId { get; set; }
        public string Title { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool Status { get; set; }
    }
}