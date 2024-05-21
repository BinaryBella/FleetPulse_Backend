using AutoMapper;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                    var user = authService.IsAuthenticated(userModel.Username, userModel.Password);

                    if (user != null)
                    {
                        if (user.JobTitle == "Admin" || user.JobTitle == "Staff")
                        {
                            var token = authService.GenerateJwtToken(user.UserName, user.JobTitle);
                            response.Data = new { token, user.JobTitle }; // Include JobTitle in the response
                            return new JsonResult(response);
                        }
                        else
                        {
                            response.Status = false;
                            response.Message = "Unauthorized: Only Admin or Staff can login";
                            return new JsonResult(response);
                        }
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

        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var user = authService.GetByUsername(model.Username);

                    if (user == null)
                    {
                        response.Status = false;
                        response.Error = "Failed to change password";
                        return new JsonResult(response);
                    }

                    var isOldPasswordValid = authService.IsAuthenticated(user.UserName, model.OldPassword);

                    if (isOldPasswordValid == null)
                    {
                        response.Status = false;
                        response.Error = "Old password is incorrect";
                        return new JsonResult(response);
                    }
                    
                    var passwordReset = authService.ResetPassword(user.EmailAddress, model.NewPassword);

                    if (passwordReset)
                    {
                        response.Message = "Password changed successfully";
                        return new JsonResult(response);
                    }
                    response.Status = false;
                    response.Error = "Failed to change password";
                    return new JsonResult(response);
                }
                response.Error = "Invalid model state";
                return BadRequest(response);
            }
            catch (Exception error)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] StaffDTO staff)
        {
            try
            {
                var oldUser = authService.GetByUsername(staff.UserName);

                if (oldUser is null)
                {
                    return NotFound("User not found");
                }
                
                oldUser.FirstName = staff.FirstName;
                oldUser.LastName = staff.LastName;
                oldUser.NIC = staff.NIC;
                oldUser.DateOfBirth = staff.DateOfBirth;
                oldUser.PhoneNo = staff.PhoneNo;
                oldUser.EmailAddress = staff.EmailAddress;
                
                if (string.IsNullOrEmpty(staff.ProfilePicture))
                { 
                    oldUser.ProfilePicture = null;
                }
                else
                {
                    oldUser.ProfilePicture = Convert.FromBase64String(staff.ProfilePicture);
                }
        
                var result = await authService.UpdateUserAsync(oldUser);

                if (result)
                {
                    return Ok("User updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to update user.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }
       
        [HttpGet("userProfile")]
        public async Task<ActionResult<StaffDTO>> GetUserByUsernameAsync(string username)
        {
            var user = await authService.GetUserByUsernameAsync(username);
    
            if (user == null)
                return NotFound();

            var profilePictureBase64 = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null;

            var StaffDTO = new StaffDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                PhoneNo = user.PhoneNo,
                NIC = user.NIC,
                ProfilePicture = profilePictureBase64
            };

            return Ok(StaffDTO);
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
