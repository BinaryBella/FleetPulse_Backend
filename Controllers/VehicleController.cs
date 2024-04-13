using System.Collections.Generic;
using System.Threading.Tasks;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;
        private readonly FleetPulseDbContext _context;

        public VehicleController(VehicleService vehicleService,FleetPulseDbContext context)
        {
            _vehicleService = vehicleService;
            _context = context;

        }

        // POST: api/Vehicle
        [HttpPost("for vehicle")]
        public async Task<ActionResult<VehicleDTO>> PostVehicle(VehicleDTO vehicle)
        {
            Vehicle model = new()
            {
                VehicleId = vehicle.VehicleId,
                VehicleRegistrationNo = vehicle.VehicleRegistrationNo,
                LicenseNo = vehicle.LicenseNo,
                LicenseExpireDate=vehicle.LicenseExpireDate,
                VehicleColor = vehicle.VehicleColor,
                Status = vehicle.Status,
                VehicleModelId = vehicle.VehicleModelId,
                VehicleTypeId = vehicle.VehicleTypeId,
                ManufactureId = vehicle.ManufactureId,
                FuelRefillId = vehicle.FuelRefillId,
            };
            _context.Vehicles.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }

        // GET: api/Vehicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehicles();           
            return Ok(vehicles);
        }
        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, VehicleDTO vehicle)
        {
            var updatedVehicle = await _vehicleService.UpdateVehicle(id, vehicle);

            if (updatedVehicle == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
