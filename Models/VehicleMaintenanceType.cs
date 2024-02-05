namespace FleetPulse_BackEndDevelopment.Models
{
    public class VehicleMaintenanceType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public bool Status { get; set; }
        
        //Vehicle_Maintenance
        public ICollection<VehicleMaintenance> VehicleMaintenances { get; set; }
    }
}