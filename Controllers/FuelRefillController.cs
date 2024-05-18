using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<FuelRefillDTO>>> GetAllFuelRefills()
        {
            var fuelRefills = await _fuelRefillService.GetAllFuelRefillsAsync();
            return Ok(fuelRefills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuelRefill>> GetFuelRefillById(int id)
        {
            var fuelRefill = await _fuelRefillService.GetFuelRefillByIdAsync(id);
            if (fuelRefill == null)
            {
                return NotFound();
            }
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
            if (id != fuelRefill.FuelRefillId)
            {
                return BadRequest();
            }

            var updated = await _fuelRefillService.UpdateFuelRefillAsync(fuelRefill);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateFuelRefill(int id)
        {
            var fuelRefill = await _fuelRefillService.GetFuelRefillByIdAsync(id);
            if (fuelRefill == null)
            {
                return NotFound();
            }

            await _fuelRefillService.DeactivateFuelRefillAsync(fuelRefill);
            return NoContent();
        }
    }
}
