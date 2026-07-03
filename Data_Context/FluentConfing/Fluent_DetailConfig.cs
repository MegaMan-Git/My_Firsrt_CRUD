using Data_Context.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
//تیک
namespace Data_Context.FluentConfig
{
    public class Fluent_DetailConfig: IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder
                .HasKey(P => P.Detail_Id);
            
            builder.Property(p => p.Detail_Color)
                // تبدیل رنگ به رشته
                .HasConversion<string>().HasMaxLength(10).IsRequired();
            builder
                .Property(p =>p.In_Stock).IsRequired();
            builder
                .Property(p => p.Detail_Description).HasMaxLength(200);

            // تعریف رابطه یک به یک
            builder
                .HasOne(p => p.product)
                .WithOne(p => p.detail)
                //کلید خارجی سمت جدول وابسته تعریف شد
                .HasForeignKey<Detail>(p => p.Detail_Id);
        }
    }
}
