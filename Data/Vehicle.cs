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
        public string FuelType { get; set; }
        public string Status { get; set; }

        // Navigation Properties
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public int VehicleModelId { get; set; }
        public VehicleModel VehicleModel { get; set; }

        public int ManufactureId { get; set; }
        public Manufacture Manufacture { get; set; }

        public int FuelRefillId { get; set; }
        public FuelRefill FuelRefill { get; set; }
    }
}
