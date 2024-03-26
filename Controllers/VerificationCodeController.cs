using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationCodeController : ControllerBase
    {
        private readonly FleetPulseDbContext _context;

        public VerificationCodeController(FleetPulseDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<VerificationCode>> GenerateVerificationCode(string email)
        {
            var verificationCode = new VerificationCode
            {
                // Code = -------(), 
                // Status = "",
                // ExpirationDateTime = DateTime.Now.AddHours(1),
                // Email = email
            };

            _context.VerificationCodes.Add(verificationCode);
            await _context.SaveChangesAsync();

            return verificationCode;
        }

        [HttpPost("validate")]
        public async Task<ActionResult<string>> ValidateVerificationCode(string email, string code)
        {
            var verificationCode = await _context.VerificationCodes
                .Where(vc => vc.Email == email && vc.Code == code && vc.Status == "Active" && vc.ExpirationDateTime > DateTime.Now)
                .FirstOrDefaultAsync();

            if (verificationCode != null)
            {
                verificationCode.Status = "Valid";
                await _context.SaveChangesAsync();
                return "Verification code is valid.";
            }
            else
            {
                return "Invalid verification code.";
            }
        }
    }
}
