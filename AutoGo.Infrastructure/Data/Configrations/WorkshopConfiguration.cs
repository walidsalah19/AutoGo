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
    public class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            // Table Name
            builder.ToTable("Workshops");

            // Primary Key
            builder.HasKey(w => w.OwnerId); // Assuming Workshop has an Id property
            builder.Ignore(x => x.Id);
            // Properties
            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(w => w.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(w => w.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(w => w.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(w => w.WebsiteUrl)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(w => w.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(w => w.EstablishedYear)
                .IsRequired();

            builder.Property(w => w.Capacity)
                .IsRequired();

            builder.Property(w => w.Specialization)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.OwnerId)
                .IsRequired();

            // Relationships
            builder.HasOne(w => w.Owner)
                .WithMany() // Or .WithMany(u => u.Workshops) if you add navigation to ApplicationUser
                .HasForeignKey(w => w.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.Reservations)
                .WithOne(r => r.Workshop)
                .HasForeignKey(r => r.WorkshopId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
