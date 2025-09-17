using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Extentions
{
    public static class FluentEmailExtention
    {
        public static void AddFluentEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSetting = configuration.GetSection("emailSetting");

            var defoultFromEmail = emailSetting["defoultFromEmail"];

            var host = emailSetting["SMTPSetting:Host"];
            var port = emailSetting.GetValue<int>("SMTPSetting:Port");

            services.AddFluentEmail(defoultFromEmail)
                .AddSmtpSender(host, port);

        }
    }
}
