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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Table name (optional)
            builder.ToTable("Payments");

            // Primary Key
            builder.HasKey(p => p.Id); // Assuming Payment has an Id property

            // Properties
            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.RentalId)
                .IsRequired(false);

            builder.Property(p => p.InvoiceId)
                .IsRequired(false);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PaymentStatus)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(p => p.User)
                .WithMany() // or .WithMany(u => u.Payments) if ApplicationUser has a collection
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

           

            //builder.HasOne(p => p.Invoice)
            //    .WithOne(i => i.Payment)
            //    .HasForeignKey<Payment>(p=>p.InvoiceId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
