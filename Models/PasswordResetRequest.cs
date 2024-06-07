using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models;

public class PasswordResetRequest
{
    [Key]
    public int Id { get; set; }
        
    [Required]
    public string Email { get; set; }
        
    public DateTime RequestedAt { get; set; }
        
    public bool IsProcessed { get; set; }
}