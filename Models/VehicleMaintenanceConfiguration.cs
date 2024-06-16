namespace FleetPulse_BackEndDevelopment.Models;

public class VehicleMaintenanceConfiguration
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public int VehicleMaintenanceTypeId { get; set; }
    public VehicleMaintenanceType VehicleMaintenanceType { get; set; }
    public string Duration { get; set; }
    public bool Status { get; set; }
}