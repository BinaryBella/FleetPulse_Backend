namespace FleetPulse_BackEndDevelopment.Models
{
    public class VehicleMaintenanceConfiguration
    {   
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public int VehicleMaintenanceTypeId { get; set; }
        public VehicleMaintenanceType VehicleMaintenanceType { get; set; }
        public string Duration { get; set; }
        public bool Status { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string DeviceToken { get; set; }
    }
}