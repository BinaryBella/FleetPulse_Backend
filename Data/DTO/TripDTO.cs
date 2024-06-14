﻿using System;

namespace FleetPulse_BackEndDevelopment.Data.DTO
{
    public class TripDTO
    {
        public string TripId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public float StartMeterValue { get; set; }
        public float EndMeterValue { get; set; }
        public bool Status { get; set; }
    }
}
