using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Common.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AutoGo.Application.Vehicles.Queries.DealerVehicle
{
    public class DealerVehiclesHandler : IRequestHandler<DealerVehiclesQuery, Result<PaginationResult<VehicleDto>>>
    {
        private readonly IVehicleCacheService _vehicleCache;
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public DealerVehiclesHandler(IVehicleCacheService vehicleCache, IAppDbContext appDbContext, IMapper mapper)
        {
            _vehicleCache = vehicleCache;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<PaginationResult<VehicleDto>>> Handle(DealerVehiclesQuery request, CancellationToken cancellationToken)
        {
            var query = _appDbContext.Vehicles
                .Include(x => x.vehicleImages)
                .AsNoTracking()
                .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                .Where(x => x.DealerId.Equals(request.DealerId));
            var vehicles =await query.ToPagedResultAsync(request.PageParameters.PageNumber, request.PageParameters.Pagesize);
            return Result<PaginationResult<VehicleDto>>.Success(vehicles);

        }
    }
}
