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
using Microsoft.Extensions.Logging;  

namespace FleetPulse_BackEndDevelopment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly FleetPulseDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService,
            IMailService mailService,
            IVerificationCodeService verificationCodeService,
            IPushNotificationService pushNotificationService,
            FleetPulseDbContext context,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _mailService = mailService;
            _verificationCodeService = verificationCodeService;
            _pushNotificationService = pushNotificationService;
            _context = context;
            _logger = logger;
            _configuration = configuration;
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
                    var user = _authService.IsAuthenticated(userModel.Username, userModel.Password);

                    if (user != null)
                    {
                        if (user.JobTitle == "Admin" || user.JobTitle == "Staff")
                        {
                            var token = _authService.GenerateJwtToken(user.UserName, user.JobTitle);
                            response.Data = new { token, user.JobTitle };
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
                var emailExists = _authService.DoesEmailExists(model.Email);

                if (!emailExists)
                {
                    response.Status = false;
                    response.Message = "Email not found";
                    return new JsonResult(response);
                }

                var verificationCode = await _verificationCodeService.GenerateVerificationCode(model.Email);
                var mailRequest = new MailRequest
                {
                    Subject = "Password Reset Verification",
                    Body = verificationCode.Code,
                    ToEmail = model.Email
                };

                await _mailService.SendEmailAsync(mailRequest);

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
            _logger.LogError(error, "An error occurred while processing the forgot password request: {Message}", error.Message); // Log error

            response.Status = false;
            response.Message = "An error occurred while processing your request";
            return StatusCode(500, response);
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

        bool isValid = await _verificationCodeService.ValidateVerificationCode(request.Email, request.Code);

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
                    var emailExists = _authService.DoesEmailExists(model.Email);

                    if (!emailExists)
                    {
                        response.Status = false;
                        response.Message = "Email not found";
                        return new JsonResult(response);
                    }

                    bool passwordReset = _authService.ResetPassword(model.Email, model.NewPassword);

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

        [HttpPost("change-password-staff")]
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
                    var user = _authService.GetByUsername(model.Username);

                    if (user == null)
                    {
                        response.Status = false;
                        response.Error = "Failed to change password";
                        return new JsonResult(response);
                    }

                    var isOldPasswordValid = _authService.IsAuthenticated(user.UserName, model.OldPassword);

                    if (isOldPasswordValid == null)
                    {
                        response.Status = false;
                        response.Error = "Old password is incorrect";
                        return new JsonResult(response);
                    }

                    var passwordReset = _authService.ResetPassword(user.EmailAddress, model.NewPassword);

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
                var oldUser = _authService.GetByUsername(staff.UserName);

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

                var result = await _authService.UpdateUserAsync(oldUser);

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
            var user = await _authService.GetUserByUsernameAsync(username);

            if (user == null)
                return NotFound();

            var profilePictureBase64 = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null;

            var staffDTO = new StaffDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                EmailAddress = user.EmailAddress,
                PhoneNo = user.PhoneNo,
                NIC = user.NIC,
                ProfilePicture = profilePictureBase64
            };

            return Ok(staffDTO);
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("localStorageKey");

            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ForgotPasswordDTO model)
        {
            var response = new ApiResponse
            {
                Status = true
            };

            try
            {
                if (ModelState.IsValid)
                {
                    var user = _context.Users.FirstOrDefault(u => u.EmailAddress == model.Email);
                    if (user == null)
                    {
                        response.Status = false;
                        response.Message = "Email not found";
                        return BadRequest(response);
                    }

                    var resetRequest = new PasswordResetRequest
                    {
                        Email = model.Email,
                        RequestedAt = DateTime.UtcNow,
                        IsProcessed = false
                    };

                    _context.PasswordResetRequests.Add(resetRequest);
                    await _context.SaveChangesAsync();

                    var userToken = GetUserToken(user.UserName);

                    var notification = new FCMNotification
                    {
                        NotificationId = Guid.NewGuid().ToString(),
                        UserId = user.UserId.ToString(),
                        Title = "Password Reset Request",
                        Message = $"Password reset requested for {user.UserName}",
                        Date = DateTime.UtcNow,
                        Time = DateTime.UtcNow.TimeOfDay,
                        Status = false
                    };

                    await _pushNotificationService.SaveNotificationAsync(notification);

                    await _pushNotificationService.SendNotificationAsync(userToken, notification.Title,
                        notification.Message, user.UserName);

                    response.Message = "Password reset request sent successfully.";
                    return Ok(response);
                }
                else
                {
                    response.Message = "Invalid model state";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        private string GetUserToken(string username)
        {
            return _configuration["FCMDeviceTokens:" + username];
        }
    }
}