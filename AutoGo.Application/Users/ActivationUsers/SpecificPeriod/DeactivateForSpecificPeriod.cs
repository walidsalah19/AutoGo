using AutoGo.Application.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Users.ActivationUsers.SpecificPeriod
{
    public class DeactivateForSpecificPeriod:IRequest<Result<string>>
    {
        public required string userId { get; set; }

        // تاريخ نهاية الحظر (لو موجود)
        public required DateTime ReactivateAt { get; set; }
    }
}
