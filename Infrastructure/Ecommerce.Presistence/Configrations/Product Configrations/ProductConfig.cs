using Ecommerce.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Configrations.Product_Configrations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                     .WithMany()
                     .HasForeignKey(p => p.TypeId);

            //builder.Property(p => p.Name)
            //       .IsRequired()
            //       .HasMaxLength(100);

            //builder.Property(p => p.Description)
            //         .IsRequired()
            //         .HasMaxLength(180);

            builder.Property(p => p.Price)
                     .IsRequired()
                     .HasColumnType("decimal(10,2)");
        }
    }
}
