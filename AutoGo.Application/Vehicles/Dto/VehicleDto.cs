using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Vehicles.Dto
{
    public class VehicleDto
    {
        public string Id { get; set; }
        public string DealerId { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public long OdometerKm { get; set; }
        public string Status { get; set; }

        public decimal DailyRate { get; set; }
        public string Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
