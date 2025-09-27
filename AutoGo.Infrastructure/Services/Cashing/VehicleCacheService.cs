using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Users.Dealer.Dtos;
using AutoGo.Application.Vehicles.Dto;
using AutoGo.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Services.Cashing
{
    public class VehicleCacheService : IVehicleCacheService
    {
        private readonly IDatabase _database;
        private readonly ILogger<VehicleCacheService> _logger;
        private RedisKey GEO_KEY = "Vehicle:geo";
        private RedisKey Set_Key= "vehicles:keys";

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
                //var images = JsonSerializer.Serialize(vehicle.Images);
                //var hashEntries = new HashEntry[]
                //{
                //    new HashEntry("Id", vehicle.Id),
                //    new HashEntry("LicensePlate", vehicle.LicensePlate),
                //    new HashEntry("Make", vehicle.Make),
                //    new HashEntry("Model", vehicle.Model),
                //    new HashEntry("Year", vehicle.Year),
                //    new HashEntry("Color", vehicle.Color),
                //    new HashEntry("VIN", vehicle.VIN),
                //    new HashEntry("OdometerKm", vehicle.OdometerKm),
                //    new HashEntry("Status", vehicle.Status),
                //    new HashEntry("DailyRate", (double)vehicle.DailyRate),
                //    new HashEntry("Category", vehicle.Category),
                //    new HashEntry("Longitude", vehicle.Longitude),
                //    new HashEntry("Latitude", vehicle.Latitude),
                //    new HashEntry("DealerId", vehicle.DealerId),
                //    new HashEntry("Images", images),

                //};

                //await _database.HashSetAsync(key, hashEntries);
                var hashEntries = new List<HashEntry>();

                var props = typeof(Vehicle).GetProperties();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(vehicle);

                    if (value == null)
                    {
                        hashEntries.Add(new HashEntry(prop.Name, string.Empty));
                        continue;
                    }

                    if (value is List<string> list)
                    {
                        // serialize the list to JSON string
                        var serializedList = JsonSerializer.Serialize(list);
                        hashEntries.Add(new HashEntry(prop.Name, serializedList));
                    }
                    else
                    {
                        hashEntries.Add(new HashEntry(prop.Name, value.ToString()));
                    }
                }

                await _database.HashSetAsync(key, hashEntries.ToArray());

                // اختياري: خزّن Key في Set لتسهيل جلب كل العربيات
                await _database.SetAddAsync(Set_Key, key);
                //إضافة ال Geo
                await _database.GeoAddAsync(GEO_KEY, (double)vehicle.Longitude, (double)vehicle.Latitude, vehicle.Id);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public async Task DeleteVehicleAsync(string id)
        {
            try
            {
                var key = $"vehicle:{id}";
                await _database.KeyDeleteAsync(key);
                await _database.SetRemoveAsync(Set_Key, key);
                await _database.GeoRemoveAsync(GEO_KEY, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = new List<VehicleDto>();

            // هات كل الـ keys من الـ Set
            var keys = await _database.SetMembersAsync("vehicles:keys");

            foreach (var key in keys)
            {

                var vehicle = await GetVehicleData(key.ToString());
                vehicles.Add(vehicle);

            }

            return vehicles;
        }

        public async Task<IEnumerable<VehicleDto>> GetNearbyVehiclesAsync(double latitude, double longitude, double radiusInKm)
        {
            var nearby = await _database.GeoRadiusAsync(GEO_KEY,longitude,latitude,radiusInKm,GeoUnit.Kilometers);
            var result = new List<VehicleDto>();
            foreach (var n in nearby)
            {
                var vehicle = await GetVehicleData($"vehicle:{n.Member.ToString()}");
                if (vehicle != null)
                    result.Add(vehicle);
            }
            return result;
        }

        public async Task<VehicleDto> GetVehicleData(string key)
        {
            var hashEntries = await _database.HashGetAllAsync(key);

            if (hashEntries.Length > 0)
            {
                var vehicle = new VehicleDto
                {
                    Id = key.ToString().Split(':')[1], // استخرج الـ ID من الـ Key
                    LicensePlate = hashEntries.FirstOrDefault(x => x.Name == "LicensePlate").Value,
                    Make = hashEntries.FirstOrDefault(x => x.Name == "Make").Value,
                    Model = hashEntries.FirstOrDefault(x => x.Name == "Model").Value,
                    Year = int.Parse(hashEntries.FirstOrDefault(x => x.Name == "Year").Value),
                    Color = hashEntries.FirstOrDefault(x => x.Name == "Color").Value,
                    VIN = hashEntries.FirstOrDefault(x => x.Name == "VIN").Value,
                    OdometerKm = long.Parse(hashEntries.FirstOrDefault(x => x.Name == "OdometerKm").Value),
                    Status = hashEntries.FirstOrDefault(x => x.Name == "Status").Value,
                    DailyRate = decimal.Parse(hashEntries.FirstOrDefault(x => x.Name == "DailyRate").Value),
                    Category = hashEntries.FirstOrDefault(x => x.Name == "Category").Value,
                    Longitude = (double)hashEntries.FirstOrDefault(x => x.Name == "Longitude").Value,
                    Latitude = (double)hashEntries.FirstOrDefault(x => x.Name == "Latitude").Value,
                    DealerId = hashEntries.FirstOrDefault(x => x.Name == "DealerId").Value,
                    Images = JsonSerializer.Deserialize<List<string>>(
                        hashEntries.FirstOrDefault(x => x.Name == "Images").Value!),
                };
                return vehicle;
            }

            return null;
        }
        public async Task UpdateFieldAsync(string id, string field, string value)
        {
            await _database.HashSetAsync($"vehicle:{id}", new HashEntry[] { new HashEntry(field, value) });
        }
        public async Task UpdateAllFieldAsync(VehicleDto vehicle)
        {
            var hashEntries = new List<HashEntry>();

            var props = typeof(Vehicle).GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(vehicle);

                if (value == null)
                {
                    hashEntries.Add(new HashEntry(prop.Name, string.Empty));
                    continue;
                }

                if (value is List<string> list)
                {
                    // serialize the list to JSON string
                    var serializedList = JsonSerializer.Serialize(list);
                    hashEntries.Add(new HashEntry(prop.Name, serializedList));
                }
                else
                {
                    hashEntries.Add(new HashEntry(prop.Name, value.ToString()));
                }
            }

            await _database.HashSetAsync($"vehicle:{vehicle.Id}", hashEntries.ToArray());
        }
    }
}
