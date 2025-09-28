using AutoGo.Application.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Domain.Enums;

namespace AutoGo.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand :IRequest<Result<string>>
    {
        public string Id { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public long OdometerKm { get; set; }
        public decimal DailyRate { get; set; }
        public VehicleCategory Category { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
