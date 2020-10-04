using eShopSolutions.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolutions.Domain.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(a => new { a.CategoryId, a.ProductId });
            builder.ToTable("ProductInCategorys");
            builder.HasOne(t => t.Product).WithMany(pc => pc.ProductInCategorys).HasForeignKey(pc => pc.ProductId);
            builder.HasOne(t => t.Category).WithMany(pc => pc.ProductInCategorys).HasForeignKey(pc => pc.CategoryId);
        }
    }
}
