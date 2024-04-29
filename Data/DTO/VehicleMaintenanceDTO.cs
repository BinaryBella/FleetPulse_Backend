namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleMaintenanceDTO
    {
        public DateTime MaintenanceDate { get; set; }
        public bool MaintenanceStatus { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PartsReplaced { get; set; }
        public string ServiceProvider { get; set; }
        public string SpecialNotes { get; set; }
        public int MaintenanceTypeId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public bool Status { get; set; }
    }
}