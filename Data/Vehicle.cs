using FleetPulse_BackEndDevelopment.Data;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string LicenseNo { get; set; }
        public DateTime LicenseExpireDate { get; set; }
        public string VehicleColor { get; set; }
        public string Status { get; set; }
        
        //Vehicle_Model
        public int VehicleModelId { get; set; }
        public VehicleModel Model { get; set; }
        //Vehicle_Type
        public int VehicleTypeId { get; set; }
        public VehicleType Type { get; set; }
        //Vehicle_Manufacture
        public int ManufactureId { get; set; }
        public Manufacture Manufacturer { get; set; }
        //FuelRefill
        public int FuelRefillId { get; set; }
        public FuelRefill FType { get; set; }
        //Vehicle_Maintenance
        public string VehicleMaintenanceId { get; set; }
        
        public VehicleMaintenance VehicleMaintenance { get; set; }

        
    }
}