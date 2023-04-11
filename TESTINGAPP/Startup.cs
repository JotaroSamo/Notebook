using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.BusinessLogic.Services;
using TESTINGAPP.Models;
using Karambolo.Extensions.Logging.File;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RecordContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add services to the container.
builder.Services.AddControllersWithViews();
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(builder.Configuration["Logging:File:Path"].ToString(), rollingInterval: RollingInterval.Day).
    CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IRecordService, RecordService>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseSession();
app.Run();
