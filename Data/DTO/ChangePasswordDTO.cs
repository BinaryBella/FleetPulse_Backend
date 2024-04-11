using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class ChangePasswordDTO
{

    [Required]
    public string Email { get; set; }

    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
    
}