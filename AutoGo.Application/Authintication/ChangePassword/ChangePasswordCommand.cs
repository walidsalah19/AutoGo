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
    public class ChangePasswordCommand : IRequest<Result<AuthResponse>>
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string UserId { get; set; }

    }
}
