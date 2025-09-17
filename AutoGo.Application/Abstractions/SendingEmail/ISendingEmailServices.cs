using AutoGo.Application.Common.Events.SendingEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.SendingEmail
{
    public interface ISendingEmailServices
    {
        public Task Send(EmailMetaData emailMetaData);

    }
}
