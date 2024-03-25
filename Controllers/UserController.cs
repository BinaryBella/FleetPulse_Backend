using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;
        private int tripid;

        public UserController(FleetPulseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.users.ToListAsync();
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(int userid)
        {
            if (userid < 1)
                return BadRequest();


            var User = await _context.users.FirstOrDefaultAsync(m => m.UserId == userid);
            if (User == null)
                return NotFound();
            return Ok(User);
        }
        [HttpPost]
        public async Task<IActionResult> Post(User User)
        {
            _context.Add(User);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(User UserData)
        {
            if (UserData == null || UserData.UserId == 0)
                return BadRequest();

            var User = await _context.users.FindAsync(UserData.UserId);
            if (User == null)
                return NotFound();
            User.UserId = UserData.UserId;
            User.FirstName = UserData.FirstName;
            User.LastName = UserData.LastName;
            User.NIC = UserData.NIC;
            User.DriverLicenseNo = UserData.DriverLicenseNo;
            User.LicenseExpiryDate = UserData.LicenseExpiryDate;
            User.BloodGroup = UserData.BloodGroup;
            User.DateOfBirth = UserData.DateOfBirth;
            User.PhoneNo = UserData.PhoneNo;
            User.UserName = UserData.UserName;
            User.HashedPassword = UserData.HashedPassword;
            User.EmailAddress = UserData.EmailAddress;
            User.EmergencyContact = UserData.EmergencyContact;
            User.JobTitle = UserData.JobTitle;
            User.ProfilePicture = UserData.ProfilePicture;
            User.Status = UserData.Status;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(int userid)
        {
            if (tripid < 1)
                return BadRequest();
            var Trip = await _context.users.FindAsync(userid);
            if (Trip == null)
                return NotFound();
            _context.users.Remove(Trip);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}


