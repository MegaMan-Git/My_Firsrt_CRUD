using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelAss.IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Context.FluentConfing.IdentityConfig
{
    public class Fluent_ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("MyRoles");

            builder.Property(p => p.ConcurrencyStamp).HasMaxLength(256);

        }
    }
}
