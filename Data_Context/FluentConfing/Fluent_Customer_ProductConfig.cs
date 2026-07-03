using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelAss.Models;
using System;
using System.Collections.Generic;
using System.Text;
//تیک
namespace Data_Context.FluentConfing
{
    public class Fluent_Customer_ProductConfig : IEntityTypeConfiguration<Customer_Product>
    {
        public void Configure(EntityTypeBuilder<Customer_Product> builder)
        {
            builder
                //کلید مرکب فقط برای کلید اصلی کاربرد دارد
                .HasKey(p => new { p.Product_id, p.Customer_id });
            builder
                .HasOne(p => p.product)
                .WithMany(p => p.Fluent_ProductCustomers)
                .HasForeignKey(p => p.Product_id);
            builder
                .HasOne(p => p.customer)
                .WithMany(p => p.Fluent_ProductCustomers)
                .HasForeignKey(p => p.Customer_id);
            
        }
    }
}
