using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class UserChangeRoleDTO
{
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string JobTitle { get; set; }
}