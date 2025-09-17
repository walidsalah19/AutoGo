using AutoGo.Application.Abstractions.AuthServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.Auth
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
            private readonly IDistributedCache distributedCache;
            private readonly IConfiguration configuration;
            private readonly ILogger<TokenBlacklistService> logger;

            public TokenBlacklistService(
                IDistributedCache distributedCache,
                IConfiguration configuration,
                ILogger<TokenBlacklistService> logger)
            {
                this.distributedCache = distributedCache;
                this.configuration = configuration;
                this.logger = logger;
            }

            public async Task<bool> IsTokenRevokedAsync(string jti)
            {
                try
                {
                    var res = await distributedCache.GetStringAsync(jti);
                    return string.IsNullOrEmpty(res);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "❌ Failed to connect to Redis while checking token status. JTI: {Jti}", jti);

                    // ممكن هنا ترجع false (تسمح للتوكن) أو true (تمنع الوصول) حسب ما تحب
                    return true;
                }
            }

            public async Task RevokeTokenAsync(string jti)
            {
                try
                {
                    var expirationHours = int.Parse(configuration["JWT:ExpireHours"]);
                    var timeSpan = TimeSpan.FromHours(expirationHours);

                    var options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = timeSpan
                    };

                    await distributedCache.SetStringAsync(jti, "revoked", options);
                    logger.LogInformation("✅ Token revoked and stored in Redis. JTI: {Jti}", jti);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "❌ Failed to connect to Redis while revoking token. JTI: {Jti}", jti);
                }
            }
        }

    }
