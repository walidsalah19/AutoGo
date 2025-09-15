using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Authintication.Dtos;
using AutoGo.Domain.Enums;
using AutoGo.Domain.Models;
using AutoGo.Infrastructure.Data.Context;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.Auth
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext appContext;

        public AuthServices(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, AppDbContext appContext)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.appContext = appContext;
        }

        public async Task<Result<AuthResponse>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
              return Result<AuthResponse>.Failure(new Error(message: "Invalid credentials", code: ErrorCodes.NotFound.ToString()));
            var result = await signInManager.CheckPasswordSignInAsync(user, currentPassword, false);

            if (!result.Succeeded)
                return Result<AuthResponse>.Failure(new Error(message: "the current password is wronge", code: ErrorCodes.Unauthorized.ToString()));

            var res = await userManager.ChangePasswordAsync(user,currentPassword,newPassword);

            if(!res.Succeeded)
            {
                var errorMessages = string.Join(", ", res.Errors.Select(e => e.Description));
                return Result<AuthResponse>.Failure(new Error(message: errorMessages, code: ErrorCodes.BadRequest.ToString()));

            }

            await ExpireTokens(userId);
            return await GenerateAuthResponse(user);

        }

        public async Task<Result<AuthResponse>> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await userManager.FindByNameAsync(usernameOrEmail)
                   ?? await userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                return Result<AuthResponse>.Failure(new Error(message: "Invalid credentials", code: ErrorCodes.NotFound.ToString()));
            else if (!user.IsActive)
                return Result<AuthResponse>.Failure(new Error(message: "Your account is not active ", code: ErrorCodes.NotFound.ToString()));

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return Result<AuthResponse>.Failure(new Error(message: "Wronge Password", code: ErrorCodes.Unauthorized.ToString()));

            return await GenerateAuthResponse(user);
        }

        public async Task<Result<string>> LogoutAsync(string userId)
        {
            await signInManager.SignOutAsync();
            await ExpireTokens(userId);
            return Result<string>.Success("Logout Successfully");
        }

        public async Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken,string userId)
        {
            var token = await appContext.RefreshTokens
                             .Include(x => x.user)
                             .FirstOrDefaultAsync(x => x.Token == refreshToken&&x.UserId.Equals(userId));

            if (token == null)
                return Result<AuthResponse>.Failure(new Error("Refresh token not found", ErrorCodes.NotFound.ToString()));

            else if (!token.user.IsActive)
                return Result<AuthResponse>.Failure(new Error(message: "Your account is not active ", code: ErrorCodes.NotFound.ToString()));

            if (token.ExpireOnUtc < DateTime.UtcNow)
                return Result<AuthResponse>.Failure(new Error("Refresh token expired", ErrorCodes.Forbidden.ToString()));

            if (token.user == null /* || !token.user.IsActive */)
                return Result<AuthResponse>.Failure(new Error("User is inactive", ErrorCodes.Forbidden.ToString()));

            var roles = await GetUserRoles(token.user);
            var accessToken = tokenService.GenerateTokens(token.user, roles);

            // Token Rotation
            token.Token = tokenService.GenerateRefreshToken();
            token.ExpireOnUtc = DateTime.UtcNow.AddDays(7);
            await appContext.SaveChangesAsync();

            accessToken.RefreshToken = token.Token;
            return Result<AuthResponse>.Success(accessToken);

        }
        private async Task<string> SaveRefreshToken(string userId)
        {
            var refrashToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokenService.GenerateRefreshToken(),
                UserId = userId,
                ExpireOnUtc = DateTime.UtcNow.AddDays(7)
            };

            await appContext.RefreshTokens.AddAsync(refrashToken);
            await appContext.SaveChangesAsync();

            return refrashToken.Token;
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        private async Task ExpireTokens(string userId)
        {
            await appContext.RefreshTokens.Where(x=> x.UserId.Equals(userId))
                .ExecuteDeleteAsync();
        }
        private async Task<Result<AuthResponse>> GenerateAuthResponse(ApplicationUser user)
        {
            var roles = await GetUserRoles(user);
            var tokens = tokenService.GenerateTokens(user, roles);
            tokens.RefreshToken = await SaveRefreshToken(user.Id);
            return Result<AuthResponse>.Success(tokens);
        }

    }
}
