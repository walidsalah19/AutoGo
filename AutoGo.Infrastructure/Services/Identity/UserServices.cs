using AutoGo.Application.Abstractions.IdentityServices;
using AutoGo.Application.Common.Events.SendingEmail;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Customers.Command.UpdateCustomer;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Models;
using AutoGo.Infrastructure.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.Identity
{
    public class UserServices : IUsersServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator mediator;
        private readonly ILogger<UserServices> _logger;


        public UserServices(UserManager<ApplicationUser> userManager, IMediator mediator, ILogger<UserServices> logger)
        {
            _userManager = userManager;
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task<Result<string>> CreateAsync(ApplicationUser user, string role, string password)
        {
            
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                return Result<string>.Failure(createResult.Errors.Select(e => e.Description).ToList());
            }
            await mediator.Publish(new SendingEmailEvent(new EmailMetaData(toAddress: user.Email, subject: "Creating account ", body: $" <h2>Welcome to AutoGo, {user.FullName}!</h2>\r\n        <p>Thank you for creating an account with us.</p>\r\n        <p>You can now log in and start using our services.</p> ")));

            await _userManager.AddToRoleAsync(user, role);
            
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
                if (result.Succeeded)
                {
                    await mediator.Publish(new SendingEmailEvent(new EmailMetaData(toAddress: user.Email, subject: "Deleting account ", body: $"  Hi, {user.FullName}! \r\n Thank you for using our system .\r\n ")));
                }
                return result.Succeeded
                    ? Result<string>.Success("User deleted successfully")
                    : Result<string>.Failure(result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception r)
            {
                throw ;
            }
        }

        public async Task<ApplicationUser> GetUserById(string userId)
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
                if (result.Succeeded)
                {
                    await mediator.Publish(new SendingEmailEvent(new EmailMetaData(toAddress: user.Email, subject: "Update account Data ", body: $"  Hi, {user.FullName}! \r\n You update your data successfully .\r\n ")));
                }
                return result.Succeeded
                    ? Result<string>.Success("User Updated successfully")
                    : Result<string>.Failure(result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception r)
            {
                throw;
            }
        }
        public async Task<Result<string>> ActivationUserAsync(ApplicationUser user, bool isActive)
        {
            var userModel = await _userManager.FindByIdAsync(user.Id);

            if (user == null)
                return Result<string>.Failure(new Error(message: "Invalid credentials", code: (int)ErrorCodes.NotFound));

            userModel.IsActive = isActive;
            await _userManager.UpdateAsync(userModel);
            await SendActivationEmail(user, isActive);
            return Result<string>.Success($"Change the {userModel.FullName} Activation status successfully");
        }

        private async Task  SendActivationEmail(ApplicationUser user ,bool isActive)
        {
            var message = isActive?"the admin active your account you can login and use our services": "the admin deactivate your account you can't login and use our services";
            
            await mediator.Publish(new SendingEmailEvent(new EmailMetaData(toAddress: user.Email, subject: "Update account Data ", body: $"  Hi, {user.FullName}! \r\n {message} .\r\n ")));
            
        }
    }
}
