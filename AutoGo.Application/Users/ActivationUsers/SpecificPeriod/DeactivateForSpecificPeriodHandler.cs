using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Abstractions.Jops;
using AutoGo.Application.Common.Result;
using MediatR;
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
            var res = await usersServices.ActivationUserAsync(userId: request.userId, isActive: false);
            await backgroundJop.DeactivateForSpecificPeriod(request);
            return res;
        }
    }
}
