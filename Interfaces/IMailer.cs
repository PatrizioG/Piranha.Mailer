using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.Mailer.Interfaces
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
