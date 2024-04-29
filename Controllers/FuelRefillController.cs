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
        public async Task<ActionResult> GetAllFuelRefillsAsync()
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
        
        [HttpPost("FuelRefill")]
        public async Task<ActionResult> AddFuelRefillAsync([FromBody] FuelRefillDTO fuelRefill)
        {
            var response = new ApiResponse();
            try
            {
                FuelRefill refill = new FuelRefill();

                refill.Date = fuelRefill.Date;
                refill.Time = fuelRefill.Time;
                refill.LiterCount = fuelRefill.LiterCount;
                refill.FType = fuelRefill.FType;
                refill.Cost = fuelRefill.Cost;
                refill.Status = fuelRefill.Status;
                refill.VehicleId = fuelRefill.VehicleId;
                // refill.DriverNic = fuelRefill.DriverNic;
                // refill.HelperNic = fuelRefill.HelperNic;
                //
                // var user = _fuelRefillService.GetByNic(fuelRefill.DriverNic);

                var addedFuelRefill = await _fuelRefillService.AddFuelRefillAsync(refill);

                if (addedFuelRefill)
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
                return new JsonResult(response);
            }

            return new JsonResult(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuelRefill(int id, FuelRefill fuelRefill)
        {
            var result = await _fuelRefillService.UpdateFuelRefillAsync(id, fuelRefill);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelRefill(int id)
        {
            var result = await _fuelRefillService.DeleteFuelRefillAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
