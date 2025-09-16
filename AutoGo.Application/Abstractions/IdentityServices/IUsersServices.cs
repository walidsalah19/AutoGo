using AutoGo.Domain.Models;
using AutoGo.Application.Common.Result;
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
        Task<Result<string>> UpdateAsync(ApplicationUser userModel);

    }
}
