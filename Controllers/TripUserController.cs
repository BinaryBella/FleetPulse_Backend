using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripUserController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public TripUserController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<TripUser>> Get()
        {
            return await _context.TripUsers.ToListAsync();
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(int userid)
        {
            if (userid < 1)
                return BadRequest();

            var TripUser = await _context.TripUsers.FirstOrDefaultAsync(m => m.userid == userid);
            if (TripUser == null)
                return NotFound();
            return Ok(TripUser);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TripUser TripUser)
        {
            _context.Add(TripUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(TripUser TripUserData)
        {
            if (TripUserData == null || TripUserData.userid == 0)
                return BadRequest();

            var TripUser = await _context.TripUsers.FindAsync(TripUserData.userid);
            if (TripUser == null)
                return NotFound();
            TripUser.UserId = TripUserData.UserId;
            TripUser.User = TripUserData.User;
            TripUser.TripId = TripUserData.TripId;
            TripUser.Trip = TripUserData.Trip;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{tripid}")]
        public async Task<IActionResult> Delete(int userid)
        {
            if (userid < 1)
                return BadRequest();
            var TripUser = await _context.TripUsers.FindAsync(userid);
            if (TripUser == null)
                return NotFound();
            _context.TripUsers.Remove(TripUser);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
