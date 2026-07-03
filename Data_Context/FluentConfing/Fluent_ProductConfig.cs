using Data_Context.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
//تیک
namespace Data_Context.FluentConfig
{
    public class Fluent_ProductConfig: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product>builder)
        {
            builder
                .HasKey(p => p.Product_Id);
            builder
                .Property(p => p.Product_Name).HasMaxLength(30).IsRequired();
            builder
                .Property(p => p.Product_Price).IsRequired();
            // رابطه یک (محصول) به چند (کتگوری) با کلید خارجی درون جدول محصول
            builder
                .HasOne(p => p.category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.Category_ID);

        }
    }
}
