/*using System.Collections.Generic;
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
    public class DriverController : ControllerBase
    {
        private readonly DriverService _driverService;
        private readonly FleetPulseDbContext _context;
        private object driver;

        public DriverController(DriverService DriverService, FleetPulseDbContext context)
        {
            _driverService = driverService;
            _context = context;

        }

        // POST: api/Driver
        [HttpPost("for driver")]
        public async Task<ActionResult<DriverDTO>> PostDriver(DriverDTO Driver)
        {
            Driver model = new()
            {
                DriverId = driver.DriverId,
                DriverFirstName = driver.DriverFirstName,
                DriverLastName = driver.DriverLastName,
                DoB = driver.DoB,
                DriverNIC = driver.DriverNIC,
                EmialAddress = driver.EmailAddress,
                PhoneNo = driver.PhoneNo,
                EmergencyContact = driver.EmergencyContact,
                BloodGroup = driver.BloodGroup,
                Status = driver.Status,
            };
            _context.Drivers.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDTO>>> GetDriver()
        {
            var drivers = await _driverService.GetAllDrivers();
            return Ok(drivers);
        }
        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, DriverDTO driver)
        {
            var updatedDriver = await _driverService.UpdateDriver(id, driver);

            if (updatedDriver == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Driver/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var result = await _driverService.DeleteDriverAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
*/
