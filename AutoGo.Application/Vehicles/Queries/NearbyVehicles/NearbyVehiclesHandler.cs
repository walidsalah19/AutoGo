using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;

namespace AutoGo.Application.Vehicles.Queries.NearbyVehicles
{
    public class NearbyVehiclesHandler : IRequestHandler<NearbyVehiclesQuery, Result<List<VehicleDto>>>
    {
        private readonly IVehicleCacheService _cacheService;

        public NearbyVehiclesHandler(IVehicleCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Result<List<VehicleDto>>> Handle(NearbyVehiclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicles =
                    await _cacheService.GetNearbyVehiclesAsync(request.latitude, request.longitude, request.radiusInKm);
                return Result<List<VehicleDto>>.Success(vehicles.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
