using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Users.Dealer.Dtos;
using MediatR;

namespace AutoGo.Application.Users.Dealer.Queries.GetNearsetDealers
{
    public class NearestDealersQuery :IRequest<Result<List<DealerDto>>>
    {
        public required double latitude { get; set; }
        public required double longitude { get; set; }
        public required double radiusInKm { get; set; }
    }
}
