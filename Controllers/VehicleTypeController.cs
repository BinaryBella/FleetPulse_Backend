using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Data.DTO;

namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VehicleTypeController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;
        public object Status { get; private set; }
        public object VehicleTypeId { get; private set; }

        public VehicleTypeController(FleetPulseDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleType(VehicleTypeDTO data)
        {
            VehicleType tmp= new VehicleType();
            tmp.Type = data.Type;
            tmp.Status=data.Status;
            await _context.VehicleType.AddAsync(tmp);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleType>> Get()

        {
            return await _context.VehicleType.ToListAsync();
        }

        [HttpGet("{vehicletypeid}")]

        public async Task<IActionResult> Get(int vehicletypeid)
        {
            if (vehicletypeid < 1)
                return BadRequest();

            var VehicleType = await _context.VehicleType.FirstOrDefaultAsync(m => m.VehicleTypeId == vehicletypeid);
            if (VehicleType == null)
                return NotFound();
            return Ok(VehicleType);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VehicleType VehicleTypeData)
        {
            if (VehicleTypeData == null || VehicleTypeData.VehicleTypeId == 0)
                return BadRequest();
            var VehicleType = await _context.VehicleType.FindAsync(VehicleTypeId);
            if (VehicleType == null)
                return NotFound();
            VehicleType.VehicleTypeId = VehicleTypeData.VehicleTypeId;
            VehicleType.Type = VehicleTypeData.Type;
            VehicleType.Status = VehicleTypeData.Status;


            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{vehicletypeid}")]

        public async Task<IActionResult> Delete(int vehicletypeid)
        {
            if (vehicletypeid < 1)
                return BadRequest();
            var VehicleType = await _context.VehicleType.FindAsync(vehicletypeid);
            if (VehicleType == null)
                return NotFound();
            _context.VehicleType.Remove(VehicleType);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}


