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
    public class RefrashTokenCommad :IRequest<Result<AuthResponse>>
    {
       public required string RefrashToken { get; set; }
       public required string UserId { get; set; }

    }
}
