using AutoGo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Vehicles.Dto;

namespace AutoGo.Application.Abstractions.Cashing
{
    public interface IVehicleCacheService
    {
        public Task CacheVehicleAsync(VehicleDto vehicle);
        public Task<List<VehicleDto>> GetAllVehiclesAsync();

    }
}
