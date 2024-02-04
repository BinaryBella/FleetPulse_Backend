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
    }
}