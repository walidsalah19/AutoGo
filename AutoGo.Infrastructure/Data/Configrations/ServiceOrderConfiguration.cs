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
    public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
    {
        public void Configure(EntityTypeBuilder<ServiceOrder> builder)
        {
            // Table Name
            builder.ToTable("ServiceOrders");

            // Primary Key
            builder.HasKey(s => s.Id); // Assuming you have Id property

            // Properties
            builder.Property(s => s.ReservationId)
                .IsRequired();

            builder.Property(s => s.ServiceStartDate)
                .IsRequired();

            builder.Property(s => s.ServiceEndDate)
                .IsRequired(false);

            builder.Property(s => s.WorkDescription)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.PartsCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.LaborCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Ignore(s => s.TotalCost); // EF will not map calculated property

            builder.Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(s => s.Reservation)
                .WithMany()
                .HasForeignKey(s => s.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.PartsUsed)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
