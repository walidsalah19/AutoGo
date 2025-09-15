using AutoGo.Application.Authintication.Dtos;
using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Authintication.Login
{
    public class LoginComand : IRequest<Result<AuthResponse>>
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }

    }
}
