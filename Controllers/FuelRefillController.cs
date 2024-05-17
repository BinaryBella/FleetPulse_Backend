using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelRefillController : ControllerBase
    {
        private readonly IFuelRefillService _fuelRefillService;

        public FuelRefillController(IFuelRefillService fuelRefillService)
        {
            _fuelRefillService = fuelRefillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelRefill>>> GetAllFuelRefillsAsync()
        {
            var fuelRefills = await _fuelRefillService.GetAllFuelRefillsAsync();
            return Ok(fuelRefills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuelRefill>> GetFuelRefillByIdAsync(int id)
        {
            var fuelRefill = await _fuelRefillService.GetFuelRefillByIdAsync(id);
            if (fuelRefill == null)
                return NotFound();

            return Ok(fuelRefill);
        }

        [HttpPost]
        public async Task<ActionResult> AddFuelRefillAsync([FromBody] FuelRefillDTO fuelRefillDTO)
        {
            var response = new ApiResponse();
            try
            {
                var fuelRefill = new FuelRefill
                {
                    Date = fuelRefillDTO.Date,
                    Time = fuelRefillDTO.Time,
                    LiterCount = fuelRefillDTO.LiterCount,
                    FType = fuelRefillDTO.FType,
                    Cost = fuelRefillDTO.Cost,
                    Status = fuelRefillDTO.Status,
                    VehicleId = fuelRefillDTO.VehicleId
                };

                var addedFuelRefill = await _fuelRefillService.AddFuelRefillAsync(fuelRefill);

                if (addedFuelRefill != null)
                {
                    response.Status = true;
                    response.Message = "Added Successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Status = false;
                    response.Message = "Failed to add Fuel Refill";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return new JsonResult(response);
        }

        [HttpPut("UpdateFuelRefill")]
        public async Task<IActionResult> UpdateFuelRefillAsync([FromBody] FuelRefillDTO fuelRefillDTO)
        {
            try
            {
                var existingFuelRefill = await _fuelRefillService.IsFuelRefillExist(fuelRefillDTO.FuelRefillId);

                if (!existingFuelRefill)
                {
                    return NotFound("Fuel Refill with Id not found");
                }

                var fuelRefill = new FuelRefill
                {
                    FuelRefillId = fuelRefillDTO.FuelRefillId,
                    Date = fuelRefillDTO.Date,
                    Time = fuelRefillDTO.Time,
                    LiterCount = fuelRefillDTO.LiterCount,
                    FType = fuelRefillDTO.FType,
                    Cost = fuelRefillDTO.Cost,
                    Status = fuelRefillDTO.Status,
                    VehicleId = fuelRefillDTO.VehicleId
                };

                var result = await _fuelRefillService.UpdateFuelRefillAsync(fuelRefill);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Fuel Refill: {ex.Message}");
            }
        }

        [HttpPut("DeactivateFuelRefill")]
        public async Task<IActionResult> DeactivateFuelRefillAsync([FromBody] FuelRefillDTO fuelRefillDTO)
        {
            try
            {
                var existingFuelRefill = await _fuelRefillService.GetFuelRefillByIdAsync(fuelRefillDTO.FuelRefillId);

                if (existingFuelRefill == null)
                    return NotFound("Fuel Refill with Id not found");

                existingFuelRefill.Status = false;

                var result = await _fuelRefillService.DeactivateFuelRefillAsync(existingFuelRefill);
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deactivating the Fuel Refill: {ex.Message}");
            }
        }
    }
}
