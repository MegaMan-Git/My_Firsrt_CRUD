using Microsoft.EntityFrameworkCore;
using Data_Context.Data;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    //Or
    //option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(

    name: "Account",
    pattern: "{controller=Account}/{action=SignUpLogin}")
    .WithStaticAssets();

//app.MapControllerRoute(
//    name: "Product",
//    pattern: "{controller=Product}/{action=Products}/{id}")
//    .WithStaticAssets();


app.Run();
