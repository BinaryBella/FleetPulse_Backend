using AutoMapper;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Helpers;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
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
        [HttpPost("login")]
        public ActionResult<ApiResponse> Login(LoginDTO userModel)
        {
            var response = new ApiResponse
            {
                Status = true
            };
            try
            {
                if (ModelState.IsValid)
                {
                    if (this.authService.IsAuthenticated(userModel.Username, userModel.Password))
                    {
                        var user = this.authService.GetByUsername(userModel.Username);
                        var token = this.authService.GenerateJwtToken(userModel.Username, user.JobTitle);
                        response.Data = token;
                        return new JsonResult(response);
                    }

                    response.Status = false;
                    response.Message = "Invalid username or password";
                    return new JsonResult(response);
                }

                response.Status = false;
                response.Error = "Invalid Data";
                return BadRequest(response);
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPost("verification-code")]
        public async Task<IActionResult> SendMail()
        {
            var apiResponse = new ApiResponse
            {
                Status = true
            };
            var verificationCode = VerificationCodeGenerator.GenerateCode();
            var mailRequest = new MailRequest
            {
                Subject = "Password Reset Verification",
                Body = "Your Verification Code is " + verificationCode,
                ToEmail = "chathushikavindya09@gmail.com"
            };
            try
            {
                await mailService.SendEmailAsync(mailRequest);
                return new JsonResult(apiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(500);
            }
        }
    }
}
