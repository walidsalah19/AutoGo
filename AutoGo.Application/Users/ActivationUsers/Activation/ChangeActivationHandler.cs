using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Common.Events.SendingEmail;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.ActivationUsers.Activation
{
    public class ChangeActivationHandler : IRequestHandler<ChangeActivationCommand, Result<string>>
    {
        private readonly IUsersServices usersServices;
        private readonly IMediator mediator;

        public ChangeActivationHandler(IUsersServices usersServices, IMediator mediator)
        {
            this.usersServices = usersServices;
            this.mediator = mediator;
        }

        public async Task<Result<string>> Handle(ChangeActivationCommand request, CancellationToken cancellationToken)
        {
            var user = await usersServices.GetUserById(request.userId);
            var res =await usersServices.ActivationUserAsync(user, isActive: request.isActive);
            await SendActivationEmail(user, request.isActive);
            return res;
        }
        private async Task SendActivationEmail(ApplicationUser user, bool isActive)
        {
            var message = isActive ? "the admin active your account you can login and use our services" : "the admin deactivate your account you can't login and use our services";

            await mediator.Publish(new SendingEmailEvent(new EmailMetaData(toAddress: user.Email, subject: "Update account Data ", body: $"  Hi, {user.FullName}! \r\n {message} .\r\n ")));

        }
    }
}
