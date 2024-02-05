namespace FleetPulse_BackEndDevelopment.Models
{
    public class Accident
    {
        public string AccidentId { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public byte[] Photos { get; set; }
        public string SpecialNotes { get; set; }
        public Decimal Loss { get; set; }
        public bool DriverInjuredStatus { get; set; }
        public bool HelperInjuredStatus { get; set; }
        public bool VehicleDamagedStatus { get; set; }
        public bool Status { get; set; }
    }
}



