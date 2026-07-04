using Microsoft.EntityFrameworkCore;
using Data_Context.FluentConfing;
using Data_Context.FluentConfing.IdentityConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Context.FluentConfig;
using ModelAss.ColorOptions_ForDetail;
using ModelAss.Models;
using Microsoft.AspNetCore.Identity;
using ModelAss.IdentityModels;

namespace Data_Context.Data
{
    public class MyDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
            
        }
        //یادم رفته بود پابلیکشون کنم
           public DbSet<ApplicationUser_Product> ApplicationUser_Products {  get; set; }
           public DbSet<Product> Products { get; set; }
           public DbSet<Category> Categories { get; set; }
           public DbSet<Detail> Details {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region کانفیگ خصوصیات مدل ها
            modelBuilder.ApplyConfiguration(new Fluent_CategoryConfig()); 
            modelBuilder.ApplyConfiguration(new Fluent_DetailConfig());
            modelBuilder.ApplyConfiguration(new Fluent_ProductConfig());
            modelBuilder.ApplyConfiguration(new Fluent_ApplicationUser_ProductsConfig());
            modelBuilder.ApplyConfiguration(new Fluent_ApplicationUserConfig());
            modelBuilder.ApplyConfiguration(new Fluent_ApplicationRoleConfig());
            modelBuilder.Entity<IdentityUserClaim<Guid>>(p => p.ToTable("MyUserClaims"));
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(p => p.ToTable("MyRoleClaims"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(p => p.ToTable("MyUserRoles"));

            modelBuilder.Entity<IdentityUserLogin<Guid>>
                (builder => 
                      { 
                            builder.ToTable("MyUserLogins");
                            builder.Property(p => p.ProviderDisplayName).HasMaxLength(100);
                      }
                );

            modelBuilder.Entity<IdentityUserToken<Guid>>
                (builder => 
                       { 
                            builder.ToTable("MyUserTokens");
                            builder.Property(p => p.Value).HasMaxLength(900);
                       }
                );
            #endregion

            #region SeedData
            modelBuilder
                .Entity<Product>()
                .HasData(
                new Product { Product_Id = 1, Product_Name = "Apple", Product_Price = 10000,
                    //کلید خارجی
                    Category_ID = 1 }
                );
            modelBuilder
                .Entity<Category>()
                .HasData(
                new Category { Category_Id = 1, Category_Name ="Fruits" }
                );
            modelBuilder
                .Entity<Detail>()
                .HasData(
                new Detail
                {
                    Detail_Id = 1,
                    Detail_Color = ColorOptions.Red,
                    In_Stock = 10,
                    Detail_Description = "Made From Mazandaran",  
                    RegistrationDate = new DateTime(2026, 3, 29, 12, 0, 0, DateTimeKind.Utc)
                }
                );
            #endregion
        }
    }
}
