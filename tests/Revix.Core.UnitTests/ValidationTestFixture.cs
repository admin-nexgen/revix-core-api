using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Revix.Core.Application.Common.Interfaces;
using Revix.Core.Api;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Revix.Core.UnitTests;

[TestClass]
public class ValidationTestFixture
{
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
        
    private const string CurrentUserId = "001";

    [TestInitialize]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        var webHostEnvironment = Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "Revix.Core.Api");

        var startup = new Startup(_configuration, webHostEnvironment);

        var services = new ServiceCollection();

        services.AddSingleton(webHostEnvironment);

        services.AddLogging();

        services.AddDistributedMemoryCache();
            
        startup.ConfigureServices(services);

        // Replace service registration for ICurrentUserService
        // Remove existing registration
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUserService));

        services.Remove(currentUserServiceDescriptor);

        // Register testing version
        services.AddTransient(provider =>
            Mock.Of<ICurrentUserService>(s => s.UserId == CurrentUserId));

        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }
}