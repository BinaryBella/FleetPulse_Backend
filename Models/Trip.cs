﻿using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Trip
    {
       
        [Key]
        public string TripId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }        
        public float StartMeterValue { get; set; }
        public float EndMeterValue { get; set; }
        public bool Status { get; set; }
        
        //Vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
        //TripUser
        public ICollection<TripUser> TripUsers { get; set; }

    }
}