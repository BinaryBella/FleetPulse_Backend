using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public VehicleController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Vehicle>> Get()
        {
            return (IEnumerable<Vehicle>)await _context.Vehicles.AsQueryable().ToListAsync();
        }
        [HttpGet("{VehicleId}")]
        public async Task<IActionResult> Get(int vehicleid)
        {
            if (vehicleid < 1)
                return BadRequest();


            var Vehicle = await _context.Vehicles.FirstOrDefaultAsync(m => m.VehicleId == vehicleid);
            if (Vehicle == null)
                return NotFound();
            return Ok(Vehicle);


        }
        [HttpPost]
        public async Task<IActionResult> Post(Vehicle Vehicle)
        {
            _context.Add(Vehicle);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Vehicle VehicleData)
        {
            if (VehicleData == null || VehicleData.VehicleId == 0)
                return BadRequest();

            var Vehicle = await _context.Vehicles.FindAsync(VehicleData.VehicleId);
            if (Vehicle == null)
                return NotFound();

            Vehicle.VehicleId = VehicleData.VehicleId;
            Vehicle.VehicleRegistrationNo = VehicleData.VehicleRegistrationNo;
            Vehicle.LicenseNo = VehicleData.LicenseNo;
            Vehicle.LicenseExpireDate = VehicleData.LicenseExpireDate;
            Vehicle.VehicleColor = VehicleData.VehicleColor;
            Vehicle.Status = VehicleData.Status;



            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{accidentid}")]
        public async Task<IActionResult> Delete(int vehicleid)

        {
            if (vehicleid < 1)
                return BadRequest();
            var Vehicle = await _context.Vehicles.FindAsync(vehicleid);
            if (Vehicle == null)
                return NotFound();
            _context.Vehicles.Remove(Vehicle);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}

