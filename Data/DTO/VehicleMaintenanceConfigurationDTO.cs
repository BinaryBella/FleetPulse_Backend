namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class VehicleMaintenanceConfigurationDTO
{
    public int VehicleId { get; set; }
    public int VehicleMaintenanceTypeId { get; set; }
    public string Duration { get; set; }
    public bool Status { get; set; }
}