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
    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            // Table name (optional)
            builder.ToTable("Dealers");

            // Primary Key
            builder.HasKey(d => d.UserId);

            builder.Ignore(d => d.Id);

            // Properties
            builder.Property(d => d.ShowroomName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Location)
                .IsRequired()
                .HasMaxLength(200);

          

            builder.Property(d => d.WebsiteUrl)
                .HasMaxLength(200);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.TaxNumber)
                .HasMaxLength(50);

            builder.Property(d => d.LicenseNumber)
                .HasMaxLength(50);

            builder.Property(d => d.EstablishedYear)
                .IsRequired();

            builder.Property(d => d.TotalVehicles)
                .IsRequired();

            // Relationships
            builder.HasOne(d => d.User)
                .WithOne() // Dealer is tied to one ApplicationUser
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}