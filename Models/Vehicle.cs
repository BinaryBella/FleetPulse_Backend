namespace FleetPulse_BackEndDevelopment.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string VehicleColor { get; set; }
        public string Status { get; set; }
        public int VehicleModelId { get; set; }
        public VehicleModel Model { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType Type { get; set; }
        public int ManufactureId { get; set; }
        public Manufacture Manufacturer { get; set; }
        public ICollection<VehicleMaintenance> VehicleMaintenance { get; set; }
        public ICollection<FuelRefill> FuelRefills { get; set; } 
        
        public ICollection<Accident> Accident { get; set; } 
        public ICollection<Trip> Trip { get; set; }
    }
}