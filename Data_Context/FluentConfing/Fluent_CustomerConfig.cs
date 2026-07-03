using Data_Context.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
//تیک
namespace Data_Context.FluentConfig
{
    public class Fluent_CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p=>p.Customer_Id);
            builder.Property(p=> p.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(p=> p.LastName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Address).HasMaxLength(50).IsRequired();
            builder.Property(p=> p.Phone).HasMaxLength(11).IsRequired();
            builder.Property(p=>p.Password).HasMaxLength(12).IsRequired();
            //  builder.HasCheckConstraint("Check_Customer_CustomerPass", "LEN(Password)>=4");

        }


    }
}
