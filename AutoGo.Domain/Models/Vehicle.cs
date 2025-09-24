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
        /// <summary>
        /// Unique license plate number of the car.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Manufacturer of the car (e.g., Toyota, BMW).
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Model of the car (e.g., Corolla, X5).
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Year of manufacture.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Color of the car.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Vehicle Identification Number (VIN) - globally unique.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Current odometer reading in kilometers.
        /// </summary>
        public long OdometerKm { get; set; }

        /// <summary>
        /// Current status of the car (Available, Rented, etc.).
        /// </summary>
        public VehicleStatus Status { get; set; } = VehicleStatus.Available;

        /// <summary>
        /// Daily rental price for this car.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyRate { get; set; }

        /// <summary>
        /// Category of the car (e.g., Economy, SUV, Luxury).
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Current location of the car.
        /// </summary>
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DealerId { get; set; }
        public Dealer Dealer { get; set; }

        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public ICollection<VehicleImage> vehicleImages { get; set; }
    }
}
