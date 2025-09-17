using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Abstractions.Jops;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.ActivationUsers.SpecificPeriod;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Jops
{
    public class BackgroundJops : IBackgroundJops
    {
        private readonly ILogger<BackgroundJops> logger;
        private readonly IUsersServices usersServices;

        public BackgroundJops(ILogger<BackgroundJops> logger, IUsersServices usersServices)
        {
            this.logger = logger;
            this.usersServices = usersServices;
        }

        public Task DeactivateForSpecificPeriod(DeactivateForSpecificPeriod period)
        {
            logger.LogInformation($"Deactivate For a Specific Period to user {period.userId}");
            var jopTime = new DateTimeOffset(period.ReactivateAt);
            BackgroundJob.Schedule(() => usersServices.ActivationUserAsync(period.userId, true), jopTime);
            return Task.CompletedTask;
        }
    }
}
