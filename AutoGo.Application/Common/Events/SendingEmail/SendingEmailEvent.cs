using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Events.SendingEmail
{
    public record SendingEmailEvent(EmailMetaData emailMetaData) :INotification;
}
