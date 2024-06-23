using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<FuelRefill>> AddFuelRefill([FromBody] FuelRefillDTO fuelRefillDto)
        {
            var addedFuelRefill = await _fuelRefillService.AddFuelRefillAsync(fuelRefillDto);
            if (addedFuelRefill == null)
            {
                return BadRequest("User or Vehicle not found");
            }
            return CreatedAtAction(nameof(GetFuelRefillById), new { id = addedFuelRefill.FuelRefillId }, addedFuelRefill);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuelRefill(int id, [FromBody] FuelRefillDTO fuelRefillDto)
        {
            var updatedFuelRefill = await _fuelRefillService.UpdateFuelRefillAsync(id, fuelRefillDto);
            if (updatedFuelRefill == null)
            {
                return NotFound("Fuel refill not found.");
            }
            return Ok(updatedFuelRefill);
        }
        
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateFuelRefill(int id)
        {
            try
            {
                await _fuelRefillService.DeactivateFuelRefillAsync(id);
                return Ok("Fuel refill deactivated successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
