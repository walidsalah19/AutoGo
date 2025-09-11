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
    public class WorkshopReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Table Name
            builder.ToTable("WorkshopReservations");

            // Primary Key
            builder.HasKey(r => r.Id); // Assuming you have Id property

            // Properties
            builder.Property(r => r.WorkshopId)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.VehicleId)
                .IsRequired();

            builder.Property(r => r.ReservationNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.ReservationDate)
                .IsRequired();

            builder.Property(r => r.AppointmentDate)
                .IsRequired();

            builder.Property(r => r.ServiceType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(r => r.ReservationStatus)
                .IsRequired();

            builder.Property(r => r.EstimatedCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(r => r.IsPaid)
                .IsRequired();

            // Relationships
            builder.HasOne(r => r.Workshop)
                .WithMany(w => w.Reservations)
                .HasForeignKey(r => r.WorkshopId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.User)
                .WithMany() // Or .WithMany(u => u.WorkshopReservations) if you add navigation to ApplicationUser
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
