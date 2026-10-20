using Berum_WebAPI.Helpers;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Berum_WebAPI.Services
{
    public class EmailService:IEmailService
    {

        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmail(
            string to,
            string subject,
            string body)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port);

            client.Credentials = new NetworkCredential(
                _settings.UserName,
                _settings.Password);

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            var mail = new MailMessage
            {
                From = new MailAddress(_settings.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            await client.SendMailAsync(mail);
        }
    }
}
