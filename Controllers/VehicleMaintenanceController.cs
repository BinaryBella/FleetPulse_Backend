// using FleetPulse_BackEndDevelopment.Data.DTO;
// using FleetPulse_BackEndDevelopment.Models;
// using FleetPulse_BackEndDevelopment.Services.Interfaces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace FleetPulse_BackEndDevelopment.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class VehicleMaintenanceController : ControllerBase
//     {
//         private readonly IVehicleMaintenanceService _maintenanceService;
//
//         public VehicleMaintenanceController(IVehicleMaintenanceService maintenanceService)
//         {
//             _maintenanceService= maintenanceService;
//         }
//
//         [HttpGet]
//         public async Task<ActionResult> GetAllVehicleMaintenancesAsync()
//         {
//             var maintenances = await _maintenanceService.GetAllVehicleMaintenancesAsync();
//             return Ok(maintenances);
//         }
//
//         [HttpGet("{id}")]
//         public async Task<ActionResult<VehicleMaintenance>> GetVehicleMaintenanceByIdAsync(string id)
//         {
//             var maintenance = await _maintenanceService.GetVehicleMaintenanceByIdAsync(id);
//             if (maintenance == null)
//                 return NotFound();
//
//             return Ok(maintenance);
//         }
//
//         [HttpPost("vehiclemaintenance")]
//         public async Task<ActionResult> AddVehicleMaintenanceAsync([FromBody] VehicleMaintenanceDTO vehicleMaintenance)
//         {
//             var response = new ApiResponse(); 
//             try
//             {
//                 var addedMaintenance = await _maintenanceService.AddVehicleMaintenanceAsync(vehicleMaintenance);
//
//                 if (addedMaintenance)
//                 {
//                     response.Status = true;
//                     response.Message = "Added Successfully";
//                     return new JsonResult(response);
//                 }
//                 else
//                 {
//                     response.Status = false;
//                     response.Message = "Failed to add Details";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 response.Status = false;
//                 response.Message = $"An error occurred: {ex.Message}";
//             }
//     
//             return new JsonResult(response);
//         }
//
//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateVehicleMaintenance(string id, VehicleMaintenance maintenance)
//         {
//             var result = await _maintenanceService.UpdateVehicleMaintenanceAsync(id, maintenance);
//             if (!result)
//                 return NotFound();
//
//             return NoContent();
//         }
//
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteVehicleMaintenance(string id)
//         {
//             var result = await _maintenanceService.DeleteVehicleMaintenanceAsync(id);
//             if (!result)
//                 return NotFound();
//
//             return NoContent();
//         }
//     }
//
// }
