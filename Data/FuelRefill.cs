namespace FleetPulse_BackEndDevelopment.Data
{
    public class FuelRefill
    {
        public int FuelRefillId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double LiterCount { get; set; }
        public string RefillType { get; set; }
        public decimal Cost { get; set; }
    }
}