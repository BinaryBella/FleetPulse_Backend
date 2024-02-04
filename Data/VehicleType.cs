namespace FleetPulse_BackEndDevelopment.Data
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}