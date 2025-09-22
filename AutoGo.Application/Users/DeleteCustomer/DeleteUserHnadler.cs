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

namespace AutoGo.Application.Users.DeleteCustomer
{
    public class DeleteUserHnadler : IRequestHandler<DeleteUserCommand, Result<string>>
    {
       

        private readonly IUsersServices _usersServices;

        public DeleteUserHnadler(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }


        public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            
               var res= await _usersServices.DeleteAsync(request.UserId);
               return res;

        }
    }
}
