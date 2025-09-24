using AutoGo.Application.Users.Dealer.Dtos;
using AutoGo.Application.Vehicles.Dto;
using AutoGo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Abstractions.Cashing
{
    public interface IVehicleCacheService
    {
        public Task CacheVehicleAsync(VehicleDto vehicle);
        public Task<List<VehicleDto>> GetAllVehiclesAsync();
        public Task<VehicleDto> GetVehicleData(string key);

        public Task DeleteVehicleAsync(string id);

        public Task<IEnumerable<VehicleDto>> GetNearbyVehiclesAsync(double latitude, double longitude,
            double radiusInKm);

    }
}
