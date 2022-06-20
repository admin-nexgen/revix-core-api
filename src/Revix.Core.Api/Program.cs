using System;
using System.Threading.Tasks;
using Destructurama;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revix.Core.Infrastructure.Persistence;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Revix.Core.Api;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = CreateLogger();

        try
        {
            var host = CreateHostBuilder(args).Build();
                
            using var scope = host.Services.CreateScope();
                
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.IsInMemoryDb)
            {
                await ApplicationInMemoryDbContextSeed.SeedDataAsync(context);
            }
                
            await host.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    
    private static Logger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {CorrelationId} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                restrictedToMinimumLevel: LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithCorrelationId()
            .CreateLogger();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseSerilog((context, logConfiguration) => logConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .Destructure.UsingAttributes()
                .Enrich.FromLogContext()
                .WriteTo.Console())
            .ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseStartup<Startup>();
            });
    }
}