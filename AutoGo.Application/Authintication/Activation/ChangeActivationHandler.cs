using AutoGo.Application.Abstractions.AuthServices;
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

namespace AutoGo.Application.Authintication.Activation
{
    public class ChangeActivationHandler : IRequestHandler<ChangeActivationCommand, Result<string>>
    {
        private readonly IAuthServices authServices;

        public ChangeActivationHandler(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        public async Task<Result<string>> Handle(ChangeActivationCommand request, CancellationToken cancellationToken)
        {

            var res =await authServices.ActivationUserAsync(userId: request.userId, isActive: request.isActive);
            return res;
        }
    }
}
