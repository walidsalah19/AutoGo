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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Table Name
            builder.ToTable("Customers");

            // Key
            builder.HasKey(c => c.userId);

            // ✅ Properties Configuration
            builder.Property(c => c.City)
                   .HasMaxLength(100)
                   .IsUnicode(true);

            builder.Property(c => c.Country)
                   .HasMaxLength(100)
                   .IsUnicode(true);

            builder.Property(c => c.DateOfBirth)
                   .HasColumnType("date"); // نحفظها كتاريخ فقط بدون وقت

           
          
            // ✅ Relationship with ApplicationUser (One-to-One)
            builder.HasOne(c => c.user)
                   .WithOne() // لو الـ ApplicationUser مش عنده Navigation Property للـ Customer
                   .HasForeignKey<Customer>(c => c.userId)
                   .OnDelete(DeleteBehavior.Cascade);

            // ✅ Relationship with Invoices (One-to-Many)
            builder.HasMany(c => c.Invoices)
                   .WithOne(i => i.customer) // Assuming Invoice has Customer navigation
                   .HasForeignKey(i => i.customerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ✅ Relationship with Rentals (One-to-Many)
            builder.HasMany(c => c.Rentals)
                   .WithOne(r => r.Customer) // Assuming Rental has Customer navigation
                   .HasForeignKey(r => r.customerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
