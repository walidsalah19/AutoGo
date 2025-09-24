using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Vehicles.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace AutoGo.Infrastructure.Services.Cashing
{
    public class VehicleCacheService : IVehicleCacheService
    {
        private readonly IDatabase _database;
        private readonly ILogger<VehicleCacheService> _logger;


        public VehicleCacheService(IConfiguration configuration, ILogger<VehicleCacheService> logger)
        {
            _database = ConnectionMultiplexer.Connect(configuration["ConnectionStrings:Redis"]).GetDatabase();
            _logger = logger;
        }

        public async Task CacheVehicleAsync(VehicleDto vehicle)
        {
            try
            {
                var key = $"vehicle:{vehicle.Id}";
                var hashEntries = new HashEntry[]
                {
                    new HashEntry("Id", vehicle.Id),
                    new HashEntry("LicensePlate", vehicle.LicensePlate),
                    new HashEntry("Make", vehicle.Make),
                    new HashEntry("Model", vehicle.Model),
                    new HashEntry("Year", vehicle.Year),
                    new HashEntry("Color", vehicle.Color),
                    new HashEntry("VIN", vehicle.VIN),
                    new HashEntry("OdometerKm", vehicle.OdometerKm),
                    new HashEntry("DailyRate",(double) vehicle.DailyRate),
                    new HashEntry("Category", vehicle.Category),
                    new HashEntry("Longitude", vehicle.Longitude),
                    new HashEntry("Latitude", vehicle.Latitude),
                    new HashEntry("DealerId", vehicle.DealerId),
                };

                await _database.HashSetAsync(key, hashEntries);

                // اختياري: خزّن Key في Set لتسهيل جلب كل العربيات
                await _database.SetAddAsync("vehicles:keys", key);
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
            }
        }

        public Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
