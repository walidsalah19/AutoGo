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
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            // Table name (optional)
            builder.ToTable("Parts");

            // Primary Key
            builder.HasKey(p => p.Id); // Assuming Part has an Id property
                                       // If not, you can use PartNumber as an alternate key.

            // Properties
            builder.Property(p => p.PartName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.PartNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Manufacturer)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.Property(p => p.MaintenanceRecordId)
                .IsRequired(false);

            // Relationships
            builder.HasOne(p => p.MaintenanceRecord)
                .WithMany(m => m.Parts)
                .HasForeignKey(p => p.MaintenanceRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
