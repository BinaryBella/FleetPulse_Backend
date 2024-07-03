using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }        
        public float StartMeterValue { get; set; }
        public float EndMeterValue { get; set; }
        public bool Status { get; set; }
        
        // Vehicle - assuming a trip can involve multiple vehicles
        public ICollection<Vehicle> Vehicles { get; set; }
        
        // TripUser - many-to-many relationship with User through TripUser
        public ICollection<TripUser> TripUsers { get; set; }  // Correctly define the navigation property
    }
}