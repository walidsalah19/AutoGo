using AutoGo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Infrastructure.Data.Configrations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            // Table name (optional)
            builder.ToTable("Vehicles");

            // Primary Key
            builder.HasKey(c => c.Id);
            builder.HasIndex(x => x.DealerId);
            builder.HasIndex(x => x.Category);

            // Properties
            builder.Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(c => c.LicensePlate)
                .IsUnique();
            builder.HasIndex(c => c.VIN)
              .IsUnique();

            builder.Property(c => c.Make)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Year)
                .IsRequired();

            builder.Property(c => c.Color)
                .HasMaxLength(30);

            builder.Property(c => c.VIN)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.OdometerKm)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.DailyRate)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Category)
                .HasMaxLength(50);

            builder.Property(c => c.Longitude)
                .IsRequired();

            builder.Property(c => c.Latitude)
                .IsRequired();

            builder.HasOne(v => v.Dealer)
                   .WithMany(d => d.Vehicles)
                   .HasForeignKey(v => v.DealerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.vehicleImages)
                   .WithOne(d => d.Vehicle)
                   .HasForeignKey(v => v.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
