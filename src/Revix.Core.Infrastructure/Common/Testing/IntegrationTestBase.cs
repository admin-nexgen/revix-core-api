using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revix.Core.Application;
using Revix.Core.Infrastructure.Mappings;

namespace Revix.Core.Infrastructure.Common.Testing;

public class IntegrationTestBase : TestBase
{
    private readonly IServiceCollection _services = new ServiceCollection();
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
        
    protected readonly ServiceProvider ServiceProvider;

    protected IntegrationTestBase()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        _configuration = builder.Build();

        _services.AddAutoMapper(typeof(CryptocurrencyProfile).Assembly);

        _services.AddApplicationServices();
        
        _services.AddInfrastructureServices(_configuration);

        ServiceProvider = _services.BuildServiceProvider();
    }
}