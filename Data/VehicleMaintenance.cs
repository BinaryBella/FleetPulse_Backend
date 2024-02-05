using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class VehicleMaintenance
    {
        public string VehicleMaintenanceId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public bool MaintenanceStatus { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PartsReplaced { get; set; }
        public string ServiceProvider { get; set; }
        public string SpecialNotes { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
        
        //Vehicle_Maintenance_Type
        public int Id { get; set; }
        public VehicleMaintenanceType TypeName { get; set; }
    }
}