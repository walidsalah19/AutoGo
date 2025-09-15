using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Authintication.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, Result<string>>
    {
        private readonly IAuthServices authServices;

        public LogoutHandler(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        public async Task<Result<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var res =await authServices.LogoutAsync(request.userId);
            return res;
        }
    }
}
