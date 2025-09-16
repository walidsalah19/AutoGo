using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Models;
using AutoGo.Infrastructure.Data.Context;
using AutoGo.Application.Common.Result;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Users.Customers.Command.UpdateCustomer;

namespace AutoGo.Infrastructure.Services.Identity
{
    public class UserServices : IUsersServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> CreateAsync(ApplicationUser user, string role, string password)
        {
            
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                return Result<string>.Failure(createResult.Errors.Select(e => e.Description).ToList());
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);
           
            return Result<string>.Success("User created successfully");
            
        }

        public async Task<Result<string>> DeleteAsync(string userId)
        {

            var user = await GetUserById(userId);

            if (user == null)
                return Result<string>.Failure(new Error(message: "User not found", code:(int) ErrorCodes.NotFound));

            try
            {
                var result = await _userManager.DeleteAsync(user);

                return result.Succeeded
                    ? Result<string>.Success("User deleted successfully")
                    : Result<string>.Failure(result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception r)
            {
                throw ;
            }
        }

        private async Task<ApplicationUser> GetUserById(string userId)
        {
           return await _userManager.FindByIdAsync(userId);
        }

        public async Task<Result<string>> UpdateAsync(UpdateCustomerCommand userModel)
        {
            var user = await GetUserById(userModel.customerId);
            if(user==null)
                return Result<string>.Failure(new Error(message: "User not found", code: (int)ErrorCodes.NotFound));
            try
            {
                user.Address = userModel.Address;
                user.PhoneNumber = userModel.PhoneNumber;
                user.UserName = userModel.FullName;
                user.Email = userModel.Email;
                user.FullName = userModel.FullName;
                
                var result = await _userManager.UpdateAsync(user);

                return result.Succeeded
                    ? Result<string>.Success("User Updated successfully")
                    : Result<string>.Failure(result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception r)
            {
                throw;
            }
        }
    }
}
