using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ManufactureController : ControllerBase

    {
        private readonly FleetPulseDbContext _context;
        private object context;

        public ManufactureController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Manufacture>> Get()
        {
            return await _context.Manufacture.ToListAsync();
        }

        [HttpGet("{manufactureid}")]
        public async Task<IActionResult> Get(int manufactureid)

        {
            if (manufactureid < 1)
                return BadRequest();




            var Manufacture = await _context.Manufacture.FirstOrDefaultAsync(m => m.ManufactureId == manufactureid);
            if (Manufacture == null)
                return NotFound();
            return Ok(Manufacture);
        }

        [HttpPost]

        public async Task<IActionResult> Post(Manufacture Manufacture)
        {
            _context.Add(Manufacture);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Manufacture ManufactureData)
        {
            if (ManufactureData == null || ManufactureData.ManufactureId == 0)
                return BadRequest();

            var Manufacture = await _context.Manufacture.FindAsync(ManufactureData.ManufactureId);
            if (Manufacture == null)
                return NotFound();
            Manufacture.ManufactureId = ManufactureData.ManufactureId;
            Manufacture.Manufacturer = ManufactureData.Manufacturer;
            Manufacture.Status = ManufactureData.Status;


            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{vehicleid}")]
        public async Task<IActionResult> Delete(int manufactureid)

        {
            if (manufactureid < 1)
                return BadRequest();
            var Manufacture = await _context.Manufacture.FindAsync(manufactureid);
            if (Manufacture == null)
                return NotFound();
            _context.Manufacture.Remove(Manufacture);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
