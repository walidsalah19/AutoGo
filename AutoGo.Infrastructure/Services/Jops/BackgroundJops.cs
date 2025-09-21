using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Abstractions.Jops;
using AutoGo.Application.Common.Events.SendingEmail;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.ActivationUsers.SpecificPeriod;
using AutoGo.Domain.Models;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.Jops
{
    public class BackgroundJops : IBackgroundJops
    {
        private readonly ILogger<BackgroundJops> logger;
        private readonly IUsersServices usersServices;
        private readonly IMediator mediator;

        public BackgroundJops(ILogger<BackgroundJops> logger, IUsersServices usersServices, IMediator mediator)
        {
            this.logger = logger;
            this.usersServices = usersServices;
            this.mediator = mediator;
        }

        public async Task DeactivateForSpecificPeriod(DeactivateForSpecificPeriod period, ApplicationUser user)
        {
            // Send first email immediately (notify about deactivation)
            await mediator.Publish(new SendingEmailEvent(
                new EmailMetaData(
                    toAddress: user.Email,
                    subject: "User Deactivation",
                    body: $"Your account has been deactivated until {period.ReactivateAt:yyyy-MM-dd HH:mm}"
                )
            ));

            logger.LogInformation($"User {period.userId} deactivated until {period.ReactivateAt:yyyy-MM-dd HH:mm}");

            // Schedule reactivation job
            var jobTime = new DateTimeOffset(period.ReactivateAt);
            var jobId = BackgroundJob.Schedule(
                () => usersServices.ActivationUserAsync(user, true),
                jobTime
            );

            logger.LogInformation($"Reactivation job scheduled for user {period.userId} at {jobTime} (JobId: {jobId})");

        }
    }
}
