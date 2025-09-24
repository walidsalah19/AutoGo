using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;

namespace AutoGo.Application.Vehicles.Queries.NearbyVehicles
{
    public class NearbyVehiclesHandler : IRequestHandler<NearbyVehiclesQuery, Result<PaginationResult<VehicleDto>>>
    {
        private readonly IVehicleCacheService _cacheService;

        public NearbyVehiclesHandler(IVehicleCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Result<PaginationResult<VehicleDto>>> Handle(NearbyVehiclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res =
                    await _cacheService.GetNearbyVehiclesAsync(request.latitude, request.longitude, request.radiusInKm);

                var vehicles =
                   await res.ToPagedResultAsync(request.PageParameters.PageNumber, request.PageParameters.Pagesize);
                return Result<PaginationResult<VehicleDto>>.Success(vehicles);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
