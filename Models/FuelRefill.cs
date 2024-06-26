﻿namespace FleetPulse_BackEndDevelopment.Models
{
    public class FuelRefill
    {
        public int FuelRefillId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double LiterCount { get; set; }
        public string FType { get; set; }
        public decimal Cost { get; set; }
        
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
        
        //FuelRefillUser
        public IList<FuelRefillUser> FuelRefillUsers { get; set; }
    }
}