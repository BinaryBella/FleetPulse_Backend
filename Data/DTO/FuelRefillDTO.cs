namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class FuelRefillDTO
    {
        public int FuelRefillId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double LiterCount { get; set; }
        public string FType { get; set; }
        public decimal Cost { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public string NIC { get; set; }
        public bool Status { get; set; }
    }
}