using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public VehicleModel VehicleModels { get; private set; }

        public VehicleModelController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<VehicleModel>> Get()
        {
            return await _context.VehicleModels.ToListAsync();
        }
        [HttpGet("{VehicleModelId}")]
        public async Task<IActionResult> Get(int VehicleModelId)
        {
            if (VehicleModelId < 1)
                return BadRequest();


            var VehicleModel = await _context.VehicleModels.FirstOrDefaultAsync(m => m.VehicleModelId == VehicleModelId);
            if (VehicleModel == null)
                return NotFound();
            return Ok(VehicleModel);
        }
        [HttpPost]
        public async Task<IActionResult> Post(VehicleModel VehicleModel)
        {
            _context.Add(VehicleModel);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Put(VehicleModel VehicleModelData)
        {
            if (VehicleModelData == null || VehicleModelData.VehicleModelId == 0)
                return BadRequest();

            var VehicleModel = await _context.VehicleModels.FindAsync(VehicleModelData.VehicleModelId);
            if (VehicleModel == null)
                return NotFound();
            VehicleModel.VehicleModelId = VehicleModelData.VehicleModelId;
            VehicleModel.Model = VehicleModelData.Model;
            VehicleModel.Status = VehicleModelData.Status;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{vehicleid}")]
        public async Task<IActionResult> Delete(int vehiclemodelid)
        {
            if (vehiclemodelid < 1)
                return BadRequest();
            var TripUser = await _context.VehicleModels.FindAsync(vehiclemodelid);
            if (TripUser == null)
                return NotFound();
            _context.VehicleModels.Remove(VehicleModels);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}

