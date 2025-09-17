using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Customers.Command.UpdateCustomer;
using AutoGo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.IdentityServices
{
    public interface IUsersServices
    {
        Task<Result<string>> CreateAsync(ApplicationUser user, string role, string password);
        Task<Result<string>> DeleteAsync(string userId);
        Task<Result<string>> UpdateAsync(UpdateCustomerCommand userModel);
        Task<Result<string>> ActivationUserAsync(ApplicationUser user, bool isActive);
        public Task<ApplicationUser> GetUserById(string userId);

    }
}
