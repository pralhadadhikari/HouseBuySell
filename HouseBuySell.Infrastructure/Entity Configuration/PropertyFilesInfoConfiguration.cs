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
    public class PropertyFilesInfoConfiguration:IEntityTypeConfiguration<PropertyFilesInfo>
    {
        public void Configure(EntityTypeBuilder<PropertyFilesInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();


            builder.HasOne(e => e.Property)
            .WithMany(pt => pt.PropertyFilesInfo)
            .HasForeignKey(e => e.PropertyId);

            builder.Property(e => e.Filename)
                .IsUnicode(true);
            builder.Property(e => e.Filepath)
                .IsUnicode(true);

            
            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");


        }
    }
}
