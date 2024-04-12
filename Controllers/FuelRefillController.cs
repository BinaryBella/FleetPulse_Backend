using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<FuelRefill>>> GetAllFuelRefills()
        {
            var fuelRefills = await _fuelRefillService.GetAllFuelRefillsAsync();
            return Ok(fuelRefills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuelRefill>> GetFuelRefillById(int id)
        {
            var fuelRefill = await _fuelRefillService.GetFuelRefillByIdAsync(id);
            if (fuelRefill == null)
                return NotFound();

            return Ok(fuelRefill);
        }

        [HttpPost]
        public async Task<ActionResult<FuelRefill>> AddFuelRefill(FuelRefill fuelRefill)
        {
            var addedFuelRefill = await _fuelRefillService.AddFuelRefillAsync(fuelRefill);
            return CreatedAtAction(nameof(GetFuelRefillById), new { id = addedFuelRefill.FuelRefillId }, addedFuelRefill);
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
