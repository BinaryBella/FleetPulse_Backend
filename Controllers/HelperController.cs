using System.Collections.Generic;
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
    public class HelperController : ControllerBase
    {
        private readonly HelperService _helperService;
        private readonly FleetPulseDbContext _context;
        private object helper;

        public HelperController(HelperService HelperService, FleetPulseDbContext context)
        {
            _helperService = helperService;
            _context = context;

        }

        // POST: api/Helper
        [HttpPost("for helper")]
        public async Task<ActionResult<HelperDTO>> PostHelper(HelperDTO Helper)
        {
            Driver model = new()
            {
                HelperId = helper.HelperId,
                HelperFirstName = helper.HelperFirstName,
                HelperLastName = helper.HelperLastName,
                DoB = helper.DoB,
                HelperNIC = helper.HelperNIC,
                EmialAddress = helper.EmailAddress,
                PhoneNo = helper.PhoneNo,
                EmergencyContact = helper.EmergencyContact,
                BloodGroup = helper.BloodGroup,
                Status = helper.Status,
            };
            _context.Helpers.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }

        // GET: api/Helper
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HelperDTO>>> GetHelper()
        {
            var helpers = await _helperService.GetAllHelpers();
            return Ok(helpers);
        }
        // PUT: api/Helper/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelper(int id, HelperDTO helper)
        {
            var updatedHelper = await _helperService.UpdateHelper(id, helper);

            if (updatedHelper == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Helper/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHelper(int id)
        {
            var result = await _helperService.DeleteHelperAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

