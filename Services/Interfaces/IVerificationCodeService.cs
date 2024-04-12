using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IVerificationCodeService
{
    Task<VerificationCode> GenerateVerificationCode(string email);
    Task<bool> ValidateVerificationCode(ValidateVerificationCodeRequest request);
    Task<bool> ValidateVerificationCode(string email, string code);
}
