using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Authintication.Logout
{
    public class LogoutCommand :IRequest<Result<string>>
    {
        public required string userId { get; set; }
    }
}
