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
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            // Table Name
            builder.ToTable("Rentals");

            // Primary Key
            builder.HasKey(r => r.Id); // assuming you have Id as primary key

            // Properties
            builder.Property(r => r.VehicleId)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.DealerId)
                .IsRequired();

          
            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired();

            builder.Property(r => r.ActualReturnDate)
                .IsRequired(false);

            builder.Property(r => r.DailyRate)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.ExtraCharges)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(r => r.Discount)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(r => r.RentalStatus)
                .IsRequired();

            builder.Property(r => r.PickupLocation)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.DropoffLocation)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.IsPaid)
                .IsRequired();

            builder.Property(r => r.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

            // Relationships
            builder.HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.User)
                .WithMany() // or .WithMany(u => u.Rentals) if you add navigation in ApplicationUser
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Dealer)
                .WithMany()
                .HasForeignKey(r => r.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

       
        }
    }
}
