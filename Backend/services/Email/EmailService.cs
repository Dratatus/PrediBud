using System.Net.Mail;
using System.Net;

namespace Backend.services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendErrorReportAsync(Exception ex)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["Email:SmtpHost"])
                {
                    Port = int.Parse(_configuration["Email:SmtpPort"]),
                    Credentials = new NetworkCredential(
                        _configuration["Email:Username"],
                        _configuration["Email:Password"]
                    ),
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Email:From"]),
                    Subject = "🚨 Application Error Report",
                    Body = $"An error occurred:\n\nMessage: {ex.Message}\n\nDate: {DateTime.Now}\n\nStackTrace:\n{ex.StackTrace}",
                    IsBodyHtml = false
                };

                _logger.LogInformation($"SMTP Host: {_configuration["Email:SmtpHost"]}");
                _logger.LogInformation($"SMTP Port: {_configuration["Email:SmtpPort"]}");
                _logger.LogInformation($"SMTP Username: {_configuration["Email:Username"]}");
                _logger.LogInformation($"SMTP Password: {_configuration["Email:Password"]}");
                _logger.LogInformation($"SMTP From: {_configuration["Email:From"]}");
                _logger.LogInformation($"SMTP Support Address: {_configuration["Email:SupportAddress"]}");

                mailMessage.To.Add(_configuration["Email:SupportAddress"]);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Error report email sent successfully.");
            }
            catch (Exception emailEx)
            {
                _logger.LogError(emailEx, "Failed to send error report email.");
            }
        }
    }
}
