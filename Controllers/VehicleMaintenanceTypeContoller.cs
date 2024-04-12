using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenanceTypeController : ControllerBase
    {
        private readonly IVehicleMaintenanceTypeService _maintenanceTypeService;

        public VehicleMaintenanceTypeController(IVehicleMaintenanceTypeService maintenanceTypeService)
        {
            _maintenanceTypeService = maintenanceTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMaintenanceType>>> GetAllVehicleMaintenanceTypes()
        {
            var maintenanceTypes = await _maintenanceTypeService.GetAllVehicleMaintenanceTypesAsync();
            return Ok(maintenanceTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMaintenanceType>> GetVehicleMaintenanceTypeById(int id)
        {
            var maintenanceType = await _maintenanceTypeService.GetVehicleMaintenanceTypeByIdAsync(id);
            if (maintenanceType == null)
                return NotFound();

            return Ok(maintenanceType);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleMaintenanceType>> AddVehicleMaintenanceType(VehicleMaintenanceType maintenanceType)
        {
            var addedMaintenanceType = await _maintenanceTypeService.AddVehicleMaintenanceTypeAsync(maintenanceType);
            return CreatedAtAction(nameof(GetVehicleMaintenanceTypeById), new { id = addedMaintenanceType.Id }, addedMaintenanceType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleMaintenanceType(int id, VehicleMaintenanceType maintenanceType)
        {
            var result = await _maintenanceTypeService.UpdateVehicleMaintenanceTypeAsync(id, maintenanceType);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMaintenanceType(int id)
        {
            var result = await _maintenanceTypeService.DeleteVehicleMaintenanceTypeAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
