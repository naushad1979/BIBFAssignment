using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Infrastructure.Repository
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            
            builder.Property(prop => prop.Id)
            .IsRequired(true)
            .HasColumnType("int").UseIdentityColumn(1);

            builder.HasKey(u => new { u.Id })
               .HasName("pk_productId");

            builder.Property(prop => prop.Name)
           .IsRequired(true)
           .HasMaxLength(20).HasColumnType("varchar(20)");

            builder.Property(prop => prop.Description)
          .IsRequired(true)
          .HasMaxLength(200).HasColumnType("varchar(200)");

            builder.Property(prop => prop.Price)
            .IsRequired(true)
            .HasMaxLength(200).HasColumnType("decimal(18,2)");

            builder.Property(prop => prop.CreatedBy)
           .IsRequired(true).HasDefaultValue("System")
           .HasColumnType("varchar(30)");

            builder.Property(prop => prop.CreatedDate)
                .IsRequired(true).HasDefaultValue(DateTime.Now).HasColumnType("datetime");

            builder.Property(prop => prop.UpdatedBy)
            .IsRequired(false)
            .HasColumnType("varchar(30)");

            builder.Property(prop => prop.UpdatedDate)
                    .IsRequired(false).HasColumnType("datetime");
        }
    }
}
