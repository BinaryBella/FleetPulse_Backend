using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services;

public interface IMailService
{ 
    Task SendEmailAsync(MailRequest mailRequest);
}