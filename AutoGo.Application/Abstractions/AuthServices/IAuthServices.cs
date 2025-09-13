using AutoGo.Application.Auth.Dtos;
using ClinicalManagement.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.AuthServices
{
    public interface IAuthServices
    {
        Task<Result<AuthResponse>> LoginAsync(string usernameOrEmail, string password);
        Task<Result<string>> LogoutAsync(string userId);
        Task<Result<AuthResponse>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken);
    }
}
