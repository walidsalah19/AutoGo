using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Dealer.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoGo.Application.Users.Dealer.Queries.GetNearsetDealers
{
    public class NearestDealersHandler : IRequestHandler<NearestDealersQuery, Result<List<DealerDto>>>
    {
        private readonly IDealerCashing _dealerCashing;
        private readonly ILogger<NearestDealersHandler> _logger;

        public NearestDealersHandler(IDealerCashing dealerCashing, ILogger<NearestDealersHandler> logger)
        {
            _dealerCashing = dealerCashing;
            _logger = logger;
        }

        public async Task<Result<List<DealerDto>>> Handle(NearestDealersQuery request, CancellationToken cancellationToken)
        {
            var dealers =await _dealerCashing.GetNearbyShowroomsAsync(request.latitude, request.longitude, request.radiusInKm);

            return Result<List<DealerDto>>.Success(dealers.ToList());
        }
    }
}
