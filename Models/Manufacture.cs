namespace FleetPulse_BackEndDevelopment.Models
{
    public class Manufacture
    {
        public int ManufactureId { get; set; }
        public string Manufacturer { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}