namespace FleetPulse_BackEndDevelopment.Models;

public class MaintenanceUser
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int VehicleMaintenanceId { get; set; }
    public  VehicleMaintenance VehicleMaintenance  { get; set; }
}