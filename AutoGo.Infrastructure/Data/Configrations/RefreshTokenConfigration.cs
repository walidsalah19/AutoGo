using ClinicalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalManagement.Infrastructure.Data.Configrations
{
    public sealed class RefreshTokenConfigration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token).HasMaxLength(225);
            builder.HasIndex(x => x.Token).IsUnique();
            builder.HasOne(x => x.user).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
