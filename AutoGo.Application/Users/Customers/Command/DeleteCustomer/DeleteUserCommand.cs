using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;
using MediatR;

namespace AutoGo.Application.Users.Customers.Command.DeleteCustomer
{
    public class DeleteUserCommand:IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }
}
