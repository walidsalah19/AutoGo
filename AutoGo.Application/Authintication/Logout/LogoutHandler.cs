using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Common.Result;
using AutoGo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AutoGo.Application.Authintication.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, Result<string>>
    {
        private readonly IAuthServices authServices;
        private readonly ITokenBlacklistService _blacklistService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LogoutHandler> logger;

        public LogoutHandler(IAuthServices authServices, ITokenBlacklistService blacklistService, IHttpContextAccessor httpContextAccessor, ILogger<LogoutHandler> logger)
        {
            this.authServices = authServices;
            _blacklistService = blacklistService;
            _httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<Result<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();

                    try
                    {
                        var jwt = handler.ReadJwtToken(token);

                        var jti = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                        if (!string.IsNullOrEmpty(jti))
                        {
                            await _blacklistService.RevokeTokenAsync(jti);
                        }
                        var res = await authServices.LogoutAsync(request.userId);
                        return res;
                    }
                    catch (Exception ex)
                    {
                        // Log error instead of throwing - token might be malformed
                        logger.LogError(ex, "Failed to read JWT token during logout.");
                        throw ex;
                    }
                }
            }

            return Result<string>.Failure(new Error(message: "Failed to logout ", code: (int)ErrorCodes.InternalServerError));
        }
    }
}
