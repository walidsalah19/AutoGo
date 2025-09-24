using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;
using AutoGo.Application.Vehicles.Dto;
using MediatR;

namespace AutoGo.Application.Vehicles.Queries.NearbyVehicles
{
    public class NearbyVehiclesQuery : IRequest<Result<List<VehicleDto>>>
    {
        public required double latitude { get; set; }
        public required double longitude { get; set; }
        public required double radiusInKm { get; set; }
    }
}
