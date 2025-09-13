using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Auth.Dtos;
using AutoGo.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.Auth
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AuthResponse GenerateTokens(ApplicationUser user, List<string> roles)
        {
            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));


            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]));

            SigningCredentials signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
            foreach (var item in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                    audience: configuration["JWT:AudienceIP"],
                    issuer: configuration["JWT:IssuerIP"],
                    expires: DateTime.Now.AddDays(7),
                    claims: userClaims,
                    signingCredentials: signing
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurity);


            return new AuthResponse
            {
                RefreshToken = "",
                AccessToken = accessToken,
                ExpiresAt = DateTime.Now.AddDays(1),
                ExpiresIn=7,
                userName=user.UserName
            };
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
