using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a=>a.Name).HasMaxLength(150).IsRequired(false);
            builder.Property(a=>a.PrictureUrl).IsRequired(false);

            builder.Property(a=>a.Price).HasPrecision(18,4);
            
            builder.HasOne(a=>a.ProductType).WithMany()
            .HasForeignKey(a=> a.ProductTypeId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a=>a.ProductBrand).WithMany()
            .HasForeignKey(a=> a.ProductBrandId).OnDelete(DeleteBehavior.Cascade);           
        }
    }
}