using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Enums;
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

        public ChangeActivationHandler(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        public async Task<Result<string>> Handle(ChangeActivationCommand request, CancellationToken cancellationToken)
        {

            var res =await usersServices.ActivationUserAsync(userId: request.userId, isActive: request.isActive);
            return res;
        }
    }
}
