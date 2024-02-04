namespace FleetPulse_BackEndDevelopment.Data
{
    public class VehicleModel
    {
        public int VehicleModelId { get; set; }
        public string Model { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }

    }
}