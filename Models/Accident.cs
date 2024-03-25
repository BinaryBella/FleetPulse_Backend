namespace FleetPulse_BackEndDevelopment.Models
{
    public class Accident
    {
        internal int accidentid;

        public int AccidentId { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public byte[] Photos { get; set; }
        public string SpecialNotes { get; set; }
        public Decimal Loss { get; set; }
        public bool DriverInjuredStatus { get; set; }
        public bool HelperInjuredStatus { get; set; }
        public bool VehicleDamagedStatus { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
        
        //AccidentUser
        public IList<AccidentUser> AccidentUsers { get; set; }
        public object VehicleId { get; internal set; }
        public object HelperId { get; internal set; }
        public object LossStatement { get; internal set; }
        public object Time { get; internal set; }
        public object DriverId { get; internal set; }
        public object Date { get; internal set; }
    }
}



