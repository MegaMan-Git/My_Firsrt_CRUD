using Data_Context.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using ModelAss.IdentityModels;
using ModelAss.PersianIdentityErrors;
using My_Firsrt_CRUD.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    //User Option
    options.User.RequireUniqueEmail = true;
    
    //SignIn Option
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    
    //Password Option
    //پ.ن: حالا سر رمز اذیت نمیشم
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    
    //Lockout Option
    options.Lockout.MaxFailedAccessAttempts = 3;
    //اتک رو میگیره ddos پ.ن: فکر کنم اگه باشه جلوی 
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrors>();
#endregion

#region Cookies
builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Account/SignUpLogin";
    });
#endregion

#region SqlServer

builder.Services.AddDbContext<MyDbContext>(option =>
{
    //option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    //Or
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion

#region DI
builder.Services.AddScoped<ViewRenderService>();
builder.Services.AddScoped<IEmailSender,EmailSender>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute
    (
    name: "Account",
    pattern: "{controller=Account}/{action=SignUpLogin}/{id?}"
    );

//app.MapControllerRoute(
//    name: "Product",
//    pattern: "{controller=Product}/{action=Products}/{id}")
//    .WithStaticAssets();

app.Run();
