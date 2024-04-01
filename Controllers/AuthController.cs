using AutoMapper;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly AuthService authService;
        private readonly IMailService mailService;
        private readonly IVerificationCodeService verificationCodeService;

        public AuthController(ILogger logger, AuthService authService, IMapper mapper, IMailService mailService,
            IVerificationCodeService verificationCodeService)
        {
            this.authService = authService;
            this.mailService = mailService;
            this.verificationCodeService = verificationCodeService;
            this.mapper = mapper;
            this.logger = logger;
        }

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

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var emailExists = authService.DoesEmailExists(model.Email);

                    if (!emailExists)
                    {
                        
                        response.Status = false;
                        response.Message = "Email not found";
                        return new JsonResult(response);
                    }

                    var verificationCode = await verificationCodeService.GenerateVerificationCode(model.Email);
                    var mailRequest = new MailRequest
                    {
                        Subject = "Password Reset Verification",
                        Body = "Your Verification Code is " + verificationCode.Code,
                        ToEmail = model.Email
                    };


                    await mailService.SendEmailAsync(mailRequest);

                    response.Message = "Verification code sent successfully";
                    return new JsonResult(response);
                }
                else
                {
                    response.Message = "Invalid model state";
                    return BadRequest(response);
                }
            }
            catch (Exception error)
            {
                return StatusCode(500);
            }
        }
        
        [AllowAnonymous]
        [HttpPost("validate-verification-code")]
        public async Task<IActionResult> ValidateVerificationCode([FromBody] ValidateVerificationCodeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ApiResponse
            {
                Status = true
            };

            bool isValid = await verificationCodeService.ValidateVerificationCode(request.Email, request.Code);

            if (isValid)
            {
                return new JsonResult(response);
            }
            else
            {
                response.Status = false;
                response.Error = "Invalid Data";
                return BadRequest(response);
            }
        }
        
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var emailExists = authService.DoesEmailExists(model.Email);

                    if (!emailExists)
                    {
                        
                        response.Status = false;
                        response.Message = "Email not found";
                        return new JsonResult(response);
                    }

                    bool passwordReset = authService.ResetPassword(model.Email, model.NewPassword);

                    if (passwordReset)
                    {
                       
                        response.Message = "Password reset successfully";
                        return new JsonResult(response);
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Failed to reset password";
                        return new JsonResult(response);
                    }
                }
                else
                {
                    response.Message = "Invalid model state";
                    return BadRequest(response);
                    
                }
            }
            catch (Exception error)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("localStorageKey");

            return RedirectToAction("Login");
        }
    }
}