using System.Net.Http.Headers;
using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Revix.Core.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Revix.Core.Application.Common.Models;
using Revix.Core.Common.HttpClients;
using Revix.Core.Domain.Clients;
using Revix.Core.Domain.Repositories;
using Revix.Core.Domain.Services;
using Revix.Core.Infrastructure.Clients;
using Revix.Core.Infrastructure.Persistence;
using Revix.Core.Infrastructure.Repositories;
using Revix.Core.Infrastructure.Services;

namespace Revix.Core.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddHttpContextAccessor();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Revix"));
        else
        {
            services.AddDbContext<ApplicationDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    a => a.MigrationsAssembly("Revix.Core.Api"))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());
        }

        services.AddAutoMapper((provider, cfg) =>
        {
            cfg.AddCollectionMappers();
            cfg.UseEntityFrameworkCoreModel<ApplicationDbContext>(provider);
        }, typeof(ApplicationDbContext).Assembly, Assembly.GetExecutingAssembly());

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddTransient<IDateTime, MachineDateTime>();
        
        services.AddTransient<ICryptocurrencyClient, CryptocurrencyClient>();
        services.AddTransient<ICryptocurrencyService, CrytocurrencyService>();
        services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();
        
        services.AddHttpClient(nameof(CryptocurrencyClient), client =>
        {
            client.BaseAddress = new System.Uri(configuration.GetValue<string>("CoinMarketCapApiUrl"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).ConfigurePrimaryHttpMessageHandler(() => new DefaultHttpClientHandler());

        return services;
    }
}