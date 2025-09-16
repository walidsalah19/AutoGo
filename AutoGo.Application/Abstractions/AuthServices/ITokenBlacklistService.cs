using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.AuthServices
{
    public interface ITokenBlacklistService
    {
        Task<bool> IsTokenRevokedAsync(string jti);
        Task RevokeTokenAsync(string jti);
    }
}
