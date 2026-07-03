using Microsoft.EntityFrameworkCore;
using Data_Context.Data;
using ModelAss.IdentityModels;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Identity
builder.Services.AddIdentity<ApplicationUser,ApplicationRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region SqlServer

builder.Services.AddDbContext<MyDbContext>(option =>
{
    //option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    //Or
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapControllerRoute
    (

    name: "Account",
    pattern: "{controller=Account}/{action=SignUpLogin}"
    );

//app.MapControllerRoute(
//    name: "Product",
//    pattern: "{controller=Product}/{action=Products}/{id}")
//    .WithStaticAssets();


app.Run();
