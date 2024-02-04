namespace FleetPulse_BackEndDevelopment.Data
{
    public class FuelRefill
    {
        public int FuelRefillId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double LiterCount { get; set; }
        public string FuelType { get; set; }
        public decimal Cost { get; set; }
        
        public bool Status { get; set; }

    }
}