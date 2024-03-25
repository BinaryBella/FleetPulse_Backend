using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public object Tripid { get; private set; }

        public TripController(FleetPulseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Trip>> Get()
        {
            return await _context.Trip.ToListAsync();
        }

        [HttpGet("{tripid}")]

        public async Task<IActionResult> Get(int tripid)
        {
            if (tripid < 1)
                return BadRequest();


            var Trip = await _context.Trip.FirstOrDefaultAsync(m => m.tripid == tripid);
            if (Trip == null)
                return NotFound();
            return Ok(Trip);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Trip Trip)
        {
            _context.Add(Trip);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Trip TripData)
        {
            if (TripData == null || TripData.tripid == 0)
                return BadRequest();

            var Trip = await _context.Trip.FindAsync(TripData.tripid);
            if (Trip == null)
                return NotFound();
            Trip.TripId = TripData.TripId;
            Trip.Date = TripData.Date;
            Trip.StartTime = TripData.StartTime;
            Trip.EndTime = TripData.EndTime;
            Trip.StartLocation = TripData.StartLocation;
            Trip.EndLocation = TripData.EndLocation;
            Trip.StartMeterValue = TripData.StartMeterValue;
            Trip.EndMeterValue = TripData.EndMeterValue;
            Trip.Status = TripData.Status;


            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{tripid}")]
        public async Task<IActionResult> Delete(int tripid)
        {
            if (tripid < 1)
                return BadRequest();
            var Trip = await _context.Trip.FindAsync(tripid);
            if (Trip == null)
                return NotFound();
            _context.Trip.Remove(Trip);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}


