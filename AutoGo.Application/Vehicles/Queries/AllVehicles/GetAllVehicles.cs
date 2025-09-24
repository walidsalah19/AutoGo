using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Pagination;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;

namespace AutoGo.Application.Vehicles.Queries.AllVehicles
{
    public class GetAllVehicles :IRequest<Result<PaginationResult<VehicleDto>>>
    {
        public required PageParameters PageParameters { get; set; }
    }
}
