using CoachAssistent.Models.ViewModels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers.Email
{
    public class SmtpUtility
    {
        readonly SmtpConfiguration _configuration;
        public SmtpUtility(SmtpConfiguration? configuration)
        {
            _configuration = configuration!;
        }
        public async Task SendMailAsync(Content content)
        {
            using var client = new SmtpClient
            {
                Host = _configuration.Host!,
                Port = _configuration.Port,
                UseDefaultCredentials = false,
                EnableSsl = _configuration.EnableSsl,
                Credentials = new NetworkCredential(_configuration.Email, _configuration.Password)
            };

            MailMessage message = new()
            {
                From = new MailAddress(_configuration.Email!),
                Subject = content.Subject,
                Body = content.Body,
                IsBodyHtml = true
            };
            message.To.Add(content.To!);
            await client.SendMailAsync(message);
        }
    }
}
