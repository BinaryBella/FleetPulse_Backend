using FleetPulse_BackEndDevelopment.Configuration;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<MailService> _logger;

        public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            // Load the HTML template
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "VerificationEmailTemplate.html");
            
            if (!File.Exists(templatePath))
            {
                _logger.LogError("Email template not found: {TemplatePath}", templatePath);
                throw new FileNotFoundException("Email template not found", templatePath);
            }

            var templateContent = await File.ReadAllTextAsync(templatePath);
            var emailBody = templateContent.Replace("{{VerificationCode}}", mailRequest.Body);
            builder.HtmlBody = emailBody;

            // Attach the logo as a linked resource
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "logo.jpg");
            var image = builder.LinkedResources.Add(logoPath);
            image.ContentId = MimeUtils.GenerateMessageId();

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            try
            {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

                // Send the email with embedded image
                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending email: {Message}", ex.Message);
                throw;
            }
        }
    }
}
