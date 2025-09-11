using AutoGo.Domain.Models;
using ClinicalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Context
{
    public interface IAppDbContext
    {
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
