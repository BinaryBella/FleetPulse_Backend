using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string? LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string? VehicleColor { get; set; }
        public string? Status { get; set; }
        
        //Vehicle_Type
        public int VehicleTypeId { get; set; }
        public VehicleType? Type { get; set; }
        //Vehicle_Manufacture
        public int ManufactureId { get; set; }
        public Manufacture? Manufacturer { get; set; }
        //FuelRefill
        public int FuelRefillId { get; set; }
        public FuelRefill? FType { get; set; }
        //Vehicle_Maintenance
        public string? VehicleMaintenanceId { get; set; }
        public VehicleMaintenance? VehicleMaintenance { get; set; }
        //Accident
        public int? AccidentId { get; set; }
        public Accident? Accident { get; set; }
        //Trip
        public string? TripId { get; set; }
        public Trip? Trip { get; set; }
        
        //Vehicle_Maintenance
        public ICollection<VehicleMaintenance> VehicleMaintenance { get; set; }
        //fuelrefill
        public ICollection<FuelRefill> FuelRefills { get; set; }
    }
}