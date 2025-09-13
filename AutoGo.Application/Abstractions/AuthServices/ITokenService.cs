using AutoGo.Application.Auth.Dtos;
using AutoGo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.AuthServices
{
    public interface ITokenService
    {
        AuthResponse GenerateTokens(ApplicationUser user, List<string> roles);
        public string GenerateRefreshToken();
    }
}
