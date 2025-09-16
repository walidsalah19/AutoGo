using AutoGo.Application.Abstractions.AuthServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
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

        public TokenBlacklistService(IDistributedCache distributedCache, IConfiguration configuration)
        {
            this.distributedCache = distributedCache;
            this.configuration = configuration;
        }

        public async Task<bool> IsTokenRevokedAsync(string jti)
        {
            var res= await distributedCache.GetStringAsync(jti);
            
            return string.IsNullOrEmpty(res);
        }

        public async Task RevokeTokenAsync(string jti)
        {
            var expirationHours = int.Parse(configuration["JWT:ExpireHours"]);
            var timeSpan = TimeSpan.FromHours(expirationHours);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeSpan
            };
            await distributedCache.SetStringAsync(jti, "revoke", options);
        }
    }
}
