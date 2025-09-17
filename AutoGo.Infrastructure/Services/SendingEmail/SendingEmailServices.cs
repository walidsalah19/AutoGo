using AutoGo.Application.Abstractions.SendingEmail;
using AutoGo.Application.Common.Events.SendingEmail;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.SendingEmail
{
    public class SendingEmailServices : ISendingEmailServices
    {
        private readonly IFluentEmail fluentEmail;
        private readonly ILogger<SendingEmailServices> logger;

        public SendingEmailServices(IFluentEmail fluentEmail, ILogger<SendingEmailServices> logger)
        {
            this.fluentEmail = fluentEmail;
            this.logger = logger;
        }

        public async Task Send(EmailMetaData emailMetaData)
        {
            try {
                await fluentEmail
                      .To(emailMetaData.ToAddress)
                      .Subject(emailMetaData.Subject)

                      .Body(emailMetaData.Body)
                      .SendAsync();
                logger.LogInformation($"sending email to {emailMetaData.ToAddress}");
            }
            catch (Exception ex)
            {
                logger.LogError($"an error occure when sending email {ex}");
                throw;
            }
        }
    }
}
