using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Accident
    {
        internal int accidentid;
        [Key]
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
        public ICollection<AccidentUser> AccidentUsers { get; set; }
        public int VehicleId { get; internal set; }
        public int HelperId { get; internal set; }
        public string LossStatement { get; internal set; }
        public DateTime Time { get; internal set; }
        public int DriverId { get; internal set; }
        public DateTime Date { get; internal set; }
    }
}



