using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

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
        public async Task<ActionResult> GetAllVehicleMaintenancesAsync()
        {
            var maintenances = await _maintenanceService.GetAllVehicleMaintenancesAsync();
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVehicleMaintenanceByIdAsync(int MaintenanceId)
        {
            var maintenance = await _maintenanceService.GetVehicleMaintenanceByIdAsync(MaintenanceId);
            if (maintenance == null)
                return NotFound();

            return Ok(maintenance);
        }

        [HttpPost("vehiclemaintenance")]
        public async Task<ActionResult> AddVehicleMaintenanceAsync([FromBody] VehicleMaintenanceDTO vehicleMaintenance)
        {
            var response = new ApiResponse();
            try
            {
                var vehicle = _maintenanceService.GetByRegNo(vehicleMaintenance.VehicleRegistrationNo);

                if (vehicle == null)
                {
                    response.Status = false;
                    response.Message = "Vehicle is not found";
                    return new JsonResult(response);
                }

                var maintenance = new VehicleMaintenance
                {
                    MaintenanceDate = vehicleMaintenance.MaintenanceDate,
                    PartsReplaced = vehicleMaintenance.PartsReplaced,
                    ServiceProvider = vehicleMaintenance.ServiceProvider,
                    Cost = vehicleMaintenance.Cost,
                    Status = vehicleMaintenance.Status,
                    SpecialNotes = vehicleMaintenance.SpecialNotes,
                    VehicleId = vehicle.VehicleId
                };

                var addedMaintenance = await _maintenanceService.AddVehicleMaintenanceAsync(maintenance);

                if (addedMaintenance)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Details";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleMaintenance(int id, [FromBody] VehicleMaintenanceDTO vehicleMaintenance)
        {
            var response = new ApiResponse();
            try
            {
                var existingMaintenance = await _maintenanceService.IsVehicleMaintenanceExist(vehicleMaintenance.MaintenanceId);

                if (!existingMaintenance)
                {
                    return NotFound("Maintenance with Id not found.");
                }

                var maintenance = new VehicleMaintenance
                {
                    MaintenanceId = vehicleMaintenance.MaintenanceId,
                    MaintenanceDate = vehicleMaintenance.MaintenanceDate,
                    PartsReplaced = vehicleMaintenance.PartsReplaced,
                    ServiceProvider = vehicleMaintenance.ServiceProvider,
                    Cost = vehicleMaintenance.Cost,
                    Status = vehicleMaintenance.Status,
                    SpecialNotes = vehicleMaintenance.SpecialNotes,
                };

                var result = await _maintenanceService.UpdateVehicleMaintenanceAsync(maintenance);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
                return new JsonResult(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMaintenance(int id)
        {
            var result = await _maintenanceService.DeleteVehicleMaintenanceAsync(id.ToString());
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
