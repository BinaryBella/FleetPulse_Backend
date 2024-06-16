using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenanceConfigurationController : ControllerBase
    {
        private readonly IVehicleMaintenanceConfigurationService _vehicleMaintenanceConfigurationService;

        public VehicleMaintenanceConfigurationController(IVehicleMaintenanceConfigurationService vehicleMaintenanceConfigurationService)
        {
            _vehicleMaintenanceConfigurationService = vehicleMaintenanceConfigurationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleMaintenanceConfiguration([FromBody] VehicleMaintenanceConfigurationDTO vehicleMaintenanceConfigurationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _vehicleMaintenanceConfigurationService.AddVehicleMaintenanceConfigurationAsync(vehicleMaintenanceConfigurationDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
