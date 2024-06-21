﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models.Reports
{
    public class VehicleManufacturerReportModel
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<Vehicle> Vehicles { get; set; }

    }
}
