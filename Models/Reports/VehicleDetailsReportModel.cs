using System;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Models.Reports
{
    public class VehicleDetailsReportModel
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}
        public List<Vehicle> Vehicles { get; set; }
        

        // Assuming Action is a placeholder, you can remove it if not needed
        // public string Action { get; set; }
    }
}
