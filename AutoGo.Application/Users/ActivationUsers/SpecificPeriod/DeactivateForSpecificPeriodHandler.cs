using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Abstractions.Jops;
using AutoGo.Application.Common.Events.SendingEmail;
using AutoGo.Application.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.ActivationUsers.SpecificPeriod
{
    public class DeactivateForSpecificPeriodHandler : IRequestHandler<DeactivateForSpecificPeriod, Result<string>>
    {
        private readonly IBackgroundJops backgroundJop;
        private readonly IUsersServices usersServices;
        public DeactivateForSpecificPeriodHandler(IBackgroundJops backgroundJop, IUsersServices usersServices)
        {
            this.backgroundJop = backgroundJop;
            this.usersServices = usersServices;
        }

        public async Task<Result<string>> Handle(DeactivateForSpecificPeriod request, CancellationToken cancellationToken)
        {
            var user = await usersServices.GetUserById(request.userId);
            var res = await usersServices.ActivationUserAsync(user, isActive: false);
            await backgroundJop.DeactivateForSpecificPeriod(request,user);
            return res;
        }
    }
}
