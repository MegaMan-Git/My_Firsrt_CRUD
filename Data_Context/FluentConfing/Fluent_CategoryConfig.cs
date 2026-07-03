using Data_Context.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
//تیک
namespace Data_Context.FluentConfig
{
    public class Fluent_CategoryConfig: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(p => p.Category_Id);
            builder
                .Property(p => p.Category_Name).HasMaxLength(30).IsRequired();

        }
    }
}
