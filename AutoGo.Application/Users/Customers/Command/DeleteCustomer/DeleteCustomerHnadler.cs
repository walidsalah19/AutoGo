using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Interfaces.UnitofWork;
using AutoGo.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoGo.Application.Users.Customers.Command.DeleteCustomer
{
    public class DeleteCustomerHnadler : IRequestHandler<DeleteCustomerCommand, Result<string>>
    {
       

        private readonly IUsersServices _usersServices;

        public DeleteCustomerHnadler(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }


        public async Task<Result<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            
               var res= await _usersServices.DeleteAsync(request.CustomerId);
               return res;

        }
    }
}
