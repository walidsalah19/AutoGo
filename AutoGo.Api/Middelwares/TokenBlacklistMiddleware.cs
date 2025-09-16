
using AutoGo.Application.Abstractions.AuthServices;
using AutoGo.Application.Common.Result;
using Azure;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace AutoGo.Api.Middelwares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public TokenBlacklistMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context, ITokenBlacklistService blacklistService)
        {
            context.Response.ContentType = "application/json";
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length);
                var handler = new JwtSecurityTokenHandler();

                try
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                    if (!string.IsNullOrEmpty(jti))
                    {
                        var isRevoked = await blacklistService.IsTokenRevokedAsync(jti);
                        if (!isRevoked)
                        {
                            var statusCode = StatusCodes.Status401Unauthorized;
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            var response= Result<string>.Failure(
                                        new Error(message: "Token has been revoked.", code: statusCode));
                            
                            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            });

                            await context.Response.WriteAsync(json);
                            return;
                        }
                    }
                }
                catch
                {
                    // لو الـ token مش صحيح نخليه يعدي على الـ Authentication middleware عشان يرفضه
                    throw;
                }

            }

            await requestDelegate(context);

        }
    }
}
