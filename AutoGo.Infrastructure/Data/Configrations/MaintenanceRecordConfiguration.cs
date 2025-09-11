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
    public class MaintenanceRecordConfiguration : IEntityTypeConfiguration<MaintenanceRecord>
    {
        public void Configure(EntityTypeBuilder<MaintenanceRecord> builder)
        {
            // Table name (optional)
            builder.ToTable("MaintenanceRecords");

            // Primary Key
            builder.HasKey(m => m.Id); 
            // Properties
            builder.Property(m => m.VehicleId)
                .IsRequired();

            builder.Property(m => m.MaintenanceDate)
                .IsRequired();

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.Cost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(m => m.PerformedBy)
                .IsRequired()
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(m => m.Vehicle)
                .WithMany()
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Workshop)
                .WithMany()
                .HasForeignKey(m => m.WorkshopId)
                .OnDelete(DeleteBehavior.SetNull);

           
        }
    }
}
