using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Piranha.Mailer.Interfaces;
using Piranha.Mailer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Mailer
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddMailerModule(this IServiceCollection services, IConfiguration smtpSettings)
        {
            App.Modules.Register<Module>();

            services.Configure<SmtpSettings>(smtpSettings.GetSection("SmtpSettings"));
            services.AddSingleton<IMailer, Mailer>();

            return services;
        }

        public static IServiceCollection AddMailerModule(this IServiceCollection services, Action<SmtpSettings> smptSettings)
        {
            App.Modules.Register<Module>();

            services.Configure(smptSettings);
            services.AddSingleton<IMailer, Mailer>();

            return services;
        }

        public static IApplicationBuilder UseMailerModule(this IApplicationBuilder builder)
        {

            return builder;
        }
    }
}
