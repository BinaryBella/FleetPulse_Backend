using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenanceController : ControllerBase
    {
        private readonly IVehicleMaintenanceService _maintenanceService;

        public VehicleMaintenanceController(IVehicleMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMaintenance>>> GetAllVehicleMaintenances()
        {
            var maintenances = await _maintenanceService.GetAllVehicleMaintenancesAsync();
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMaintenance>> GetVehicleMaintenanceById(string id)
        {
            var maintenance = await _maintenanceService.GetVehicleMaintenanceByIdAsync(id);
            if (maintenance == null)
                return NotFound();

            return Ok(maintenance);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleMaintenance>> AddVehicleMaintenance(VehicleMaintenance maintenance)
        {
            var addedMaintenance = await _maintenanceService.AddVehicleMaintenanceAsync(maintenance);
            return CreatedAtAction(nameof(GetVehicleMaintenanceById), new { id = addedMaintenance.VehicleMaintenanceId }, addedMaintenance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleMaintenance(string id, VehicleMaintenance maintenance)
        {
            var result = await _maintenanceService.UpdateVehicleMaintenanceAsync(id, maintenance);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMaintenance(string id)
        {
            var result = await _maintenanceService.DeleteVehicleMaintenanceAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
