using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentsController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public AccidentsController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Accident>> Get()
        {
            return await _context.Accidents.AsQueryable().ToListAsync();
        }
        [HttpGet("{accidentid}")]
        public async Task<IActionResult> Get(int accidentid)
        {
            if (accidentid < 1)
                return BadRequest();


            var Accident = await _context.Accidents.FirstOrDefaultAsync(m => m.accidentid == accidentid);
            if (Accident == null)
                return NotFound();
            return Ok(Accident);


        }
        [HttpPost]
        public async Task<IActionResult> Post(Accident Accident)
        {
            _context.Add(Accident);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Accident AccidentData)
        {
            if (AccidentData == null || AccidentData.accidentid == 0)
                return BadRequest();

            var Accident = await _context.Accidents.FindAsync(AccidentData.accidentid);
            if (Accident == null)
                return NotFound();
            Accident.Date = AccidentData.Date;
            Accident.Time = AccidentData.Time;
            Accident.Venue = AccidentData.Venue;
            Accident.Status = AccidentData.Status;
            Accident.DriverInjuredStatus = AccidentData.DriverInjuredStatus;
            Accident.HelperInjuredStatus = AccidentData.HelperInjuredStatus;
            Accident.VehicleDamagedStatus = AccidentData.VehicleDamagedStatus;
            Accident.LossStatement = AccidentData.LossStatement;
            Accident.SpecialNotes = AccidentData.SpecialNotes;
            Accident.Photos = AccidentData.Photos;
            Accident.DriverId = AccidentData.DriverId;
            Accident.HelperId = AccidentData.HelperId;
            Accident.VehicleId = AccidentData.VehicleId;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{accidentid}")]
        public async Task<IActionResult> Delete(int accidentid)

        {
            if (accidentid < 1)
                return BadRequest();
            var Accident = await _context.Accidents.FindAsync(accidentid);
            if (Accident == null)
                return NotFound();
            _context.Accidents.Remove(Accident);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}

