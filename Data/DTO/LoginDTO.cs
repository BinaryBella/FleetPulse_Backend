using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class LoginDTO
{
    [EmailAddress]
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }
}