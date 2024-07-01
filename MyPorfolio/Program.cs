using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MyPorfolio.Apis;
using MyPorfolio.Middlewares;
using MyPorfolio.Models.Context;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace MyPorfolio;

public class Program
{
    public static MyBot mybot = new MyBot();
    public static MyFile api_file = new MyFile();
    public static void Main(string[] args)
    {

        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
           .WriteTo.File("logs/mylog.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MyPorfolioContext>(
            s => s.UseSqlite(builder.Configuration.GetConnectionString("default")));

        builder.Services.AddAuthentication
            (CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(c =>
                {
                    c.LoginPath = "/Security/Login";
                });

        builder.Services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });

        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<MyBot>();

        var app = builder.Build();
        Log.Information($"Application started {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseExceptionHandlerMiddleware();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Site}/{action=Index}/{id?}");

        mybot.start();

        app.Run();
    }
}
