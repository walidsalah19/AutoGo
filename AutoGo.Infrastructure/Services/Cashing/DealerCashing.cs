using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoGo.Application.Abstractions.Cashing;
using AutoGo.Application.Users.Dealer.Dtos;
using AutoGo.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace AutoGo.Infrastructure.Services.Cashing
{
    public class DealerCashing:IDealerCashing
    {
        private readonly IDatabase _database;
        private readonly ILogger<DealerCashing> _logger;

        private RedisKey GEO_KEY = "Dealer:geo";

        public DealerCashing(IConfiguration configuration, ILogger<DealerCashing> _logger)
        {
            _database = ConnectionMultiplexer.Connect(configuration["ConnectionStrings:Redis"]).GetDatabase();

            this._logger = _logger;
        }

        public async Task CacheDealerAsync(DealerDto dealer)
        {
            try
            {
                await _database.GeoAddAsync(GEO_KEY,(double) dealer.Longitude, (double)dealer.Latitude, dealer.Id);
                var json = JsonSerializer.Serialize(dealer);
                await  _database.HashSetAsync($"dealer:{dealer.Id}", new HashEntry[]
                {
                    new HashEntry("UserName", dealer.UserName ?? ""),
                    new HashEntry("Address", dealer.Address ?? ""),
                    new HashEntry("ShowroomName", dealer.ShowroomName ?? ""),
                    new HashEntry("Email", dealer.Email ?? ""),
                    new HashEntry("PhoneNumber", dealer.PhoneNumber ?? ""),
                    new HashEntry("WebsiteUrl", dealer.WebsiteUrl ?? ""),
                    new HashEntry("Description", dealer.Description ?? ""),
                    new HashEntry("TaxNumber", dealer.TaxNumber ?? ""),
                    new HashEntry("LicenseNumber", dealer.LicenseNumber ?? ""),
                    new HashEntry("EstablishedYear", dealer.EstablishedYear),
                    new HashEntry("TotalVehicles", dealer.TotalVehicles ),
                    new HashEntry("Latitude",(double) dealer.Latitude),
                    new HashEntry("Longitude", (double) dealer.Longitude),
                    new HashEntry("IsActive", dealer.IsActive)
                });
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
                
            }
        }
        public async Task<DealerDto?> GetDealerAsync(string id)
        {
            try
            {


                var entries = await _database.HashGetAllAsync($"dealer:{id}");
                if (entries.Length == 0) return null;

                return new DealerDto
                {
                    Id = id,
                    UserName = entries.FirstOrDefault(x => x.Name == "FullName").Value,
                    Address = entries.FirstOrDefault(x => x.Name == "Address").Value,
                    ShowroomName = entries.FirstOrDefault(x => x.Name == "ShowroomName").Value,
                    Email = entries.FirstOrDefault(x => x.Name == "Email").Value,
                    PhoneNumber = entries.FirstOrDefault(x => x.Name == "PhoneNumber").Value,
                    WebsiteUrl = entries.FirstOrDefault(x => x.Name == "WebsiteUrl").Value,
                    Description = entries.FirstOrDefault(x => x.Name == "Description").Value,
                    TaxNumber = entries.FirstOrDefault(x => x.Name == "TaxNumber").Value,
                    LicenseNumber = entries.FirstOrDefault(x => x.Name == "LicenseNumber").Value,
                    EstablishedYear = (int)entries.FirstOrDefault(x => x.Name == "EstablishedYear").Value,
                    TotalVehicles = (int)entries.FirstOrDefault(x => x.Name == "TotalVehicles").Value,
                    Latitude = (double)entries.FirstOrDefault(x => x.Name == "Latitude").Value,
                    Longitude = (double)entries.FirstOrDefault(x => x.Name == "Longitude").Value,
                    IsActive = (bool)entries.FirstOrDefault(x => x.Name == "IsActive").Value
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
        public async Task<IEnumerable<DealerDto>> GetNearbyShowroomsAsync(double latitude, double longitude, double radiusInKm)
        {

            var nearby = await _database.GeoRadiusAsync(GEO_KEY, longitude, latitude, radiusInKm, GeoUnit.Kilometers);

            var result = new List<DealerDto>();
            foreach (var n in nearby)
            {
                var showroom = await GetDealerAsync(n.Member.ToString());
                if (showroom != null)
                    result.Add(showroom);
            }
            return result;
        }
        // ✅ تحديث خاصية معينة بدون تحميل الكائن كامل
        public async Task UpdateFieldAsync(string id, string field, string value)
        {
            await _database.HashSetAsync($"dealer:{id}", new HashEntry[] { new HashEntry(field, value) });
        }
    }
}
