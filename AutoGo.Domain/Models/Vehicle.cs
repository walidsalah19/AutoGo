using AutoGo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Domain.Models
{
    public  class Vehicle :BaseEntity
    {
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public long OdometerKm { get; set; }
        public VehicleStatus Status { get; set; } = VehicleStatus.Available;
        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyRate { get; set; }
        public string Category { get; set; } // e.g., Economy, SUV, Luxury
        public string CurrentLocation { get; set; }

        public string DealerId { get; set; }
        public Dealer Dealer { get; set; }

        public ICollection<VehicleImage> vehicleImages { get; set; }
    }
}
