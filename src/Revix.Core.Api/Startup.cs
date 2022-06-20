using Revix.Core.Application.Common.Interfaces;
using Revix.Core.Infrastructure;
using Revix.Core.Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Revix.Core.Api.Services;
using Revix.Core.Application;
using Serilog;

namespace Revix.Core.Api;

public class Startup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Log.Logger);
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        services
            .AddCustomCors()
            .AddCustomMvc()
            .AddCustomApiFeatures()
            .AddCustomSwagger()
            .AddCustomHealthChecks()
            .AddFeatureManagement();

        services.AddApplicationServices();
        
        services.AddInfrastructureServices(_configuration);
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app
            .UseHttpsRedirection()
            .UseSerilogRequestLogging()
            .UseCustomCors()
            .UseAuthentication()
            .UseRouting()
            .UseCustomSwagger(provider)
            .UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health",
                    new HealthCheckOptions
                    {
                        Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                endpoints.MapHealthChecks("/liveness",
                    new HealthCheckOptions
                    {
                        Predicate = _ => true
                    });
                endpoints.MapControllers();
            });
    }
}