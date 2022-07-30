using App.Domain.Core.Entities;
using App.InfraStructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    //options.AccessDeniedPath = 

});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var connectionString = builder.Configuration.GetConnectionString("FinalProject");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));


builder.Services.AddIdentity<AppUser, IdentityRole<int>>(
    options =>
    {

        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequiredLength = 2;
    })
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Area",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
