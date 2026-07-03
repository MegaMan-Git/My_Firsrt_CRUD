using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelAss.IdentityModels;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Data_Context.FluentConfing.IdentityConfig
{
    public class Fluent_ApplicationUserConfig  : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("MyUsers");

            builder.Property(p => p.UserName).HasMaxLength(128).IsRequired();
            builder.Property(p => p.NormalizedUserName).HasMaxLength(128).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.NormalizedEmail).HasMaxLength(100);
            builder.Property(p => p.PasswordHash).HasMaxLength(128);


            builder.Property(p => p.SecurityStamp).HasMaxLength(256);
            builder.Property(p => p.ConcurrencyStamp).HasMaxLength(256);

            //+98 0912~~~~~~~
            builder.Property(p => p.PhoneNumber).HasMaxLength(14);
        }
    }
}
