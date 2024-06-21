using FleetPulse_BackEndDevelopment.Models.Reports;
using FleetPulse_BackEndDevelopment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleReportsController : ControllerBase
    {
        private readonly IVehicleReportService _vehicleReportService;

        public VehicleReportsController(IVehicleReportService vehicleReportService)
        {
            _vehicleReportService = vehicleReportService;
        }

        [HttpGet("details")]
        public async Task<ActionResult<List<VehicleDetailsReportModel>>> GetVehicleDetailsReport()
        {
            var vehicleDetailsReport = await _vehicleReportService.GetVehicleDetailsReportAsync();
            //return Ok(vehicleDetailsReport);
            var detailreport = new VehicleDetailsReportModel();
            detailreport.StartDate = ;
            detailreport.EndDate = ;
            detailreport.Vehicles = vehicleDetailsReport;

        }

        [HttpGet("fuel-refill")]
        public async Task<ActionResult<List<VehicleFuelRefillReportModel>>> GetVehicleFuelRefillReport()
        {
            var vehicleFuelRefillReport = await _vehicleReportService.GetVehicleFuelRefillReportAsync();
            return Ok(vehicleFuelRefillReport);
        }

        [HttpGet("maintenance")]
        public async Task<ActionResult<List<VehicleMaintenanceReportModel>>> GetVehicleMaintenanceReport()
        {
            var vehicleMaintenanceReport = await _vehicleReportService.GetVehicleMaintenanceReportAsync();
            return Ok(vehicleMaintenanceReport);
        }

        // Add other report endpoints as needed...
    }
}
