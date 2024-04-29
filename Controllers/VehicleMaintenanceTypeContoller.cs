using FleetPulse_BackEndDevelopment.Data.DTO;
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
        public async Task<ActionResult<VehicleMaintenanceType>> AddVehicleMaintenanceType(
            [FromBody] string maintenanceType)
        {
            VehicleMaintenanceType? vehicleMaintenanceType = new VehicleMaintenanceType();
            vehicleMaintenanceType.TypeName = maintenanceType;

            var addedMaintenanceType =
                await _maintenanceTypeService.AddVehicleMaintenanceTypeAsync(vehicleMaintenanceType);
            return CreatedAtAction(nameof(GetVehicleMaintenanceTypeById), new { id = addedMaintenanceType.Id },
                addedMaintenanceType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicleMaintenanceType(
            [FromBody] VehicleMaintenanceTypeDTO maintenanceType)
        {
            try
            {
                var existingMaintenanceType =
                    await _maintenanceTypeService.GetVehicleMaintenanceTypeByIdAsync(maintenanceType.Id);
                

                if (existingMaintenanceType == null)
                    return NotFound($"MaintenanceType with Id not found");
                var vehicleMaintenanceType = new VehicleMaintenanceType
                {
                    Id = maintenanceType.Id,
                    TypeName = maintenanceType.TypeName,
                    Status = maintenanceType.Status
                };
                var result = await _maintenanceTypeService.UpdateVehicleMaintenanceTypeAsync(vehicleMaintenanceType);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the maintenance type: {ex.Message}");
            }
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