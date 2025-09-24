using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using Microsoft.Extensions.Logging;

namespace AutoGo.Application.Vehicles.Queries.AllVehicles
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehicles, Result<List<VehicleDto>>>
    {
        private readonly IVehicleCacheService _cacheService;
        private readonly ILogger<GetAllVehiclesHandler> _logger;

        public GetAllVehiclesHandler(IVehicleCacheService cacheService, ILogger<GetAllVehiclesHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<List<VehicleDto>>> Handle(GetAllVehicles request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicles = await _cacheService.GetAllVehiclesAsync();

                return Result<List<VehicleDto>>.Success(vehicles);
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
