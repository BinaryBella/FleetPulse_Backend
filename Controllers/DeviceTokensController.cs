using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTokensController : ControllerBase
    {
        private readonly IDeviceTokenService _deviceTokenService;

        public DeviceTokensController(IDeviceTokenService deviceTokenService)
        {
            _deviceTokenService = deviceTokenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceToken>>> GetDeviceTokens()
        {
            var tokens = await _deviceTokenService.GetAllTokensAsync();
            return Ok(tokens);
        }

        [HttpPost]
        public async Task<ActionResult> AddDeviceToken([FromBody] DeviceToken token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _deviceTokenService.AddTokenAsync(token);
            return CreatedAtAction(nameof(GetDeviceTokens), new { id = token.Id }, token);
        }
    }
}
