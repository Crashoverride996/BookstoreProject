using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configuration
{
    public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
    {
        public void Configure(EntityTypeBuilder<Accessory> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(25);
            builder.Property(a => a.Description).HasMaxLength(100);
            builder.Property(a => a.Price).HasDefaultValue(0);
        }
    }
}
