using AutoMapper;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly AuthService authService;

        public AuthController(ILogger logger, AuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<string> Login(LoginDTO userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (this.authService.IsAuthenticated(userModel.EmailAddress, userModel.Password))
                    {
                        var user = this.authService.GetByEmail(userModel.EmailAddress);
                        var token = this.authService.GenerateJwtToken(userModel.EmailAddress, user.JobTitle);
                        return Ok(token);
                    }

                    return BadRequest("Email or password are not correct!");
                }

                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }
    }
}
