using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
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
        public async Task<ActionResult> AddVehicleMaintenanceTypeAsync(
            [FromBody] VehicleMaintenanceTypeDTO maintenanceType)
        {
            var response = new ApiResponse();
            try
            {
                var vehicleMaintenanceType = new VehicleMaintenanceType
                {
                    TypeName = maintenanceType.TypeName
                };

                var vehicleMaintenanceTypeExists =
                    _maintenanceTypeService.DoesVehicleMaintenanceTypeExists(maintenanceType.TypeName);
                if (vehicleMaintenanceTypeExists)
                {
                    response.Message = "Vehicle Maintenance Type already exist";
                    return new JsonResult(response);
                }

                var addedMaintenanceType =
                    await _maintenanceTypeService.AddVehicleMaintenanceTypeAsync(vehicleMaintenanceType);

                if (addedMaintenanceType != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Maintenance Type";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }


        [HttpPut("UpdateVehicleMaintenanceType")]
        public async Task<IActionResult> UpdateVehicleMaintenanceType(
            [FromBody] VehicleMaintenanceTypeDTO maintenanceType)
        {
            try
            {
                var existingMaintenanceType = await _maintenanceTypeService.IsVehicleTypeExist(maintenanceType.Id);

                if (!existingMaintenanceType)
                {
                    return NotFound("MaintenanceType with Id not found");
                }
                
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

        [HttpPut("DeactivateVehicleMaintenanceType")]
        public async Task<IActionResult> DeactivateVehicleMaintenanceType(
            [FromBody] VehicleMaintenanceTypeDTO maintenanceType)
        {
            try
            {
                var existingMaintenanceType =
                    await _maintenanceTypeService.GetVehicleMaintenanceTypeByIdAsync(maintenanceType.Id);

                if (existingMaintenanceType == null)
                    return NotFound("MaintenanceType with Id not found");

                var result =
                    await _maintenanceTypeService.DeactivateVehicleMaintenanceTypeAsync(existingMaintenanceType);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    $"An error occurred while deactivating the vehicle maintenance type: {ex.Message}");
            }
        }
    }
}