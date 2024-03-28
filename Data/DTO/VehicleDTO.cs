namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string VehicleColor { get; set; }
        public string Status { get; set; }
        public int VehicleModelId { get; set; }
        public int VehicleTypeId { get; set; }
        public int ManufactureId { get; set; }
        public int FuelRefillId { get; set; }
        public string VehicleMaintenanceId { get; set; }
        public int? AccidentId { get; set; }
        public string TripId { get; set; }
    }
}
