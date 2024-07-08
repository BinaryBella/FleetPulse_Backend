namespace FleetPulse_BackEndDevelopment.DTOs
{
    public class TripDTO
    {
        public string NIC { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public float StartMeterValue { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public float EndMeterValue { get; set; }
        public bool Status { get; set; }
    }
}
