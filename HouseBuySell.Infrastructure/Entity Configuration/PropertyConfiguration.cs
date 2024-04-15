using HouseBuySell.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuySell.Infrastructure.Entity_Configuration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();


            builder.HasOne(e => e.PropertyType)        
            .WithMany(pt => pt.Property)        
            .HasForeignKey(e => e.PropertyTypeId);

            builder.Property(e => e.Price)
               .HasColumnType("float");

            builder.Property(e => e.Features)
                .IsUnicode(true);
            builder.Property(e => e.Location)
                .IsUnicode(true);

            builder.Property(e => e.ImageFullPath)
               .HasMaxLength(255);



            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedDate)               
                .HasColumnType("datetime");

        }
    }
}
