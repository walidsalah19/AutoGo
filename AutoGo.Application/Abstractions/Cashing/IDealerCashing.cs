using AutoGo.Application.Users.Dealer.Dtos;
using AutoGo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.Cashing
{
    public interface IDealerCashing
    {
        public Task CacheDealerAsync(DealerDto dealer);
        public Task<DealerDto?> GetDealerAsync(string id);

        public Task<IEnumerable<DealerDto>> GetNearbyShowroomsAsync(double latitude, double longitude,
            double radiusInKm);

        public Task UpdateFieldAsync(string id, string field, string value);

    }
}
