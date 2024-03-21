using AutoMapper;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
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
        private readonly IMailService mailService;

        public AuthController(ILogger logger, AuthService authService, IMapper mapper, IMailService mailService)
        {
            this.authService = authService;
            this.mapper = mapper;
            this.logger = logger;
            this.mailService = mailService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<string> Login(LoginDTO userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (this.authService.IsAuthenticated(userModel.Username, userModel.Password))
                    {
                        var user = this.authService.GetByUsername(userModel.Username);
                        var token = this.authService.GenerateJwtToken(userModel.Username, user.JobTitle);
                        return Ok(token);
                    }

                    return new JsonResult("Username or password are not correct!");
                }

                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm]MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
