using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Authintication.Dtos;
using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Authintication.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefrashTokenCommad, Result<AuthResponse>>
    {
        private readonly IAuthServices authServices;

        public RefreshTokenHandler(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        public async Task<Result<AuthResponse>> Handle(RefrashTokenCommad request, CancellationToken cancellationToken)
        {
            var res = await authServices.RefreshTokenAsync(request.RefrashToken, request.UserId);
            return res;
        }
    }
}
