using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelAss.Models;
using System;
using System.Collections.Generic;
using System.Text;
//تیک
namespace Data_Context.FluentConfing
{
    public class Fluent_ApplicationUser_ProductsConfig : IEntityTypeConfiguration<ApplicationUser_Product>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser_Product> builder)
        {
            builder
                //کلید مرکب فقط برای کلید اصلی کاربرد دارد
                .HasKey(p => new { p.userId, p.productId });
            builder
                .HasOne(p => p.User)
                .WithMany(p => p.ApplicationUserProducts)
                .HasForeignKey(p => p.userId);
            builder
                .HasOne(p => p.Product)
                .WithMany(p => p.ApplicationUserProducts)
                .HasForeignKey(p => p.productId);
            
        }
    }
}
