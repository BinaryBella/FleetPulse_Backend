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
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;
        private readonly FleetPulseDbContext _context;
        private object staff;

        public StaffController(StaffService StaffService, FleetPulseDbContext context)
        {
            _staffService = staffService;
            _context = context;

        }

        // POST: api/Staff
        [HttpPost("for staff")]
        public async Task<ActionResult<StaffDTO>> PostStaff(StaffDTO Staff)
        {
            Staff model = new()
            {
                StaffId = staff.HelperId,
                StaffFirstName = staff.StaffFirstName,
                StaffLastName = staff.StaffLastName,
                DoB = staff.DoB,
                StaffNIC = staff.StaffNIC,
                EmialAddress = staff.EmailAddress,
                PhoneNo = staff.PhoneNo,
                EmergencyContact = staff.EmergencyContact,
                JobTitle = staff.JobTitleGroup,
                Status = staff.Status,
            };
            _context.Staffs.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDTO>>> GetStaff()
        {
            var helpers = await _staffService.GetAllStaffs();
            return Ok(staffs);
        }
        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelper(int id, StaffDTO staff)
        {
            var updatedStaff = await _staffService.UpdateStaff(id, staff);

            if (updatedStaff == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _staffService.DeleteStaffAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
*/
