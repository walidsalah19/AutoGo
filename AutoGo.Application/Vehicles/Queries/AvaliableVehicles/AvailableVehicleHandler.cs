using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Context;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using AutoGo.Application.Vehicles.Queries.AllVehicles;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Domain.Enums;
using AutoMapper.QueryableExtensions;

namespace AutoGo.Application.Vehicles.Queries.AvaliableVehicles
{
    public class AvailableVehicleHandler : IRequestHandler<AvaliableVehiclesQuery, Result<PaginationResult<VehicleDto>>>
    {
        private readonly IVehicleCacheService _cacheService;
        private readonly ILogger<GetAllVehiclesHandler> _logger;
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AvailableVehicleHandler(IVehicleCacheService cacheService, ILogger<GetAllVehiclesHandler> logger, IAppDbContext appDbContext, IMapper mapper)
        {
            _cacheService = cacheService;
            _logger = logger;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginationResult<VehicleDto>>> Handle(AvaliableVehiclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _cacheService.GetAllVehiclesAsync();
                if (res.Count != 0)
                {
                    _logger.LogInformation("vehicles from Redis");
                    var vehicles = await
                        res.Where(x=>x.Status.Equals(VehicleStatus.Available.ToString())).ToPagedResultAsync(request.PageParameters.PageNumber,
                        request.PageParameters.Pagesize);
                    return Result<PaginationResult<VehicleDto>>.Success(vehicles);
                }
                _logger.LogInformation("vehicles from Db");
                var query = _appDbContext.Vehicles
                    .AsNoTracking()
                    .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                    .Where(x => x.Status.Equals(VehicleStatus.Available.ToString()));
                var v = await query.ToPagedResultAsync(request.PageParameters.PageNumber,
                    request.PageParameters.Pagesize);
                return Result<PaginationResult<VehicleDto>>.Success(v);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
