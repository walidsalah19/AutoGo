using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Vehicles.Queries.AllVehicles
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehicles, Result<PaginationResult<VehicleDto>>>
    {
        private readonly IVehicleCacheService _cacheService;
        private readonly ILogger<GetAllVehiclesHandler> _logger;

        public GetAllVehiclesHandler(IVehicleCacheService cacheService, ILogger<GetAllVehiclesHandler> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<Result<PaginationResult<VehicleDto>>> Handle(GetAllVehicles request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _cacheService.GetAllVehiclesAsync();
                var vehicles = await res.ToPagedResultAsync(request.PageParameters.PageNumber,
                    request.PageParameters.Pagesize);
                return Result< PaginationResult<VehicleDto>>.Success(vehicles);
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
