using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Piranha.Mailer.Interfaces;
using Piranha.Mailer.Models;
using System;
using System.Threading.Tasks;

namespace Piranha.Mailer
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;

        public Mailer(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                message.Body = new TextPart(TextFormat.Html) { Text = body };

                using(var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    smtp.MessageSent += (sender, a) =>
                    {
                    };

                    await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
