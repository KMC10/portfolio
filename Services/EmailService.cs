using KMC.Portfolio.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace KMC.Portfolio.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task SendEmailAsync(string senderEmail, string senderName, string message)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Khulani Dlamini", _smtpSettings.Username));
            email.ReplyTo.Add(new MailboxAddress(senderName, senderEmail));
            email.To.Add(new MailboxAddress("Khulani Dlamini", _smtpSettings.Username));

            email.Subject = "Portfolio Contact Message";

            email.Body = new TextPart("plain")
            {
                Text = message
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _smtpSettings.Host,
                _smtpSettings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _smtpSettings.Username,
                _smtpSettings.Password);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}