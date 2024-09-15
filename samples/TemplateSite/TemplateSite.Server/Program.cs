using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TemplateSite.Server.Components;

namespace TemplateSite.Server;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .CreateBootstrapLogger();
        
        try
        {
            var app = await CreateWebApplication(args);
            
            Log.Information("Application user name: " + GetApplicationUserName());

            var runtimeVersion = RuntimeInformation.FrameworkDescription;
            
            Log.Information("Runtime version: " + runtimeVersion);

            await app.RunAsync();
            
            Log.Information("Host terminated clearly");
            
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return -1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static async Task<WebApplication> CreateWebApplication(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();
        
        var builder = WebApplication.CreateBuilder(args);
        
        builder.WebHost.UseConfiguration(configuration);
        
        builder.Host.UseSerilog((context, services, serilogConfiguration) =>
        {
            serilogConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        });

        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        return app;
    }
    
    private static string GetApplicationUserName()
    {
        return string.Join("\\", Environment.UserDomainName, Environment.UserName);
    }
}