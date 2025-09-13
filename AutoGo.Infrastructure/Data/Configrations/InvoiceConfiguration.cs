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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            // Table name (optional)
            builder.ToTable("Invoices");

            // Primary Key
            builder.HasKey(i => i.InvoiceNumber);

            // Properties
            builder.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.IssueDate)
                .IsRequired();

            builder.Property(i => i.DueDate)
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.PaidAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Ignore(i => i.RemainingAmount); // Computed property - no DB column

            builder.Property(i => i.Status)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
           

            builder.HasOne(i => i.Rental)
                .WithMany()
                .HasForeignKey(i => i.RentalId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.ServiceOrder)
                .WithMany()
                .HasForeignKey(i => i.ServiceOrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
