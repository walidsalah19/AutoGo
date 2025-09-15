using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Authintication.Dtos;
using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Authintication.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<AuthResponse>>
    {
        private readonly IAuthServices authServices;

        public ChangePasswordHandler(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        public async Task<Result<AuthResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await authServices.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);
            return res;
        }
    }
}
