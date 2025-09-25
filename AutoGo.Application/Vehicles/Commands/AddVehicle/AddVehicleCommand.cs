using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGo.Application.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AutoGo.Application.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommand :IRequest<Result<string>>
    {
        public string DealerId { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public long OdometerKm { get; set; }
        public decimal DailyRate { get; set; }
        public string Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
