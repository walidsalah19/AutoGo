using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Abstractions.SendingEmail;
using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Events.SendingEmail
{
    public class SendingEmailEventHandler : INotificationHandler<SendingEmailEvent>
    {
        private readonly ISendingEmailServices sendingEmail;

        public SendingEmailEventHandler(ISendingEmailServices sendingEmail)
        {
            this.sendingEmail = sendingEmail;
        }

        public async Task Handle(SendingEmailEvent notification, CancellationToken cancellationToken)
        {

            BackgroundJob.Enqueue(() => sendingEmail.Send(notification.emailMetaData));
        }
    }
}
