using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Revix.Core.Common.Configurations;
using Revix.Core.Common.Extensions;
using Revix.Core.Common.HttpClients;

namespace Revix.Core.Infrastructure.Common.Testing;

/// <summary>
/// Base class for end to end tests ie. Tests where we restfully hit the endpoint interrogating response codes
/// </summary>
public class FunctionalTestBase : TestBase
{
    protected const string ClientName = " FunctionalTest";
    
    private readonly IConfiguration _configuration;
    private readonly IServiceCollection _services = new ServiceCollection();
    private readonly IHttpClientFactory _clientFactory;
        
    protected string BaseUrl { get; }
        
    protected string TestClientId { get; }
        
    // ReSharper disable once MemberCanBePrivate.Global
    protected bool TestLocally { get; }

    // ReSharper disable once MemberCanBePrivate.Global
    protected ServiceProvider ServiceProvider { get; }

    protected FunctionalTestBase()
    {
        TestLocally = false;
            
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        _configuration = builder.Build();
            
        _services.AddHttpContextAccessor();
        
        #region  Cache
        
        var cacheOptions = new CacheOptions();
        _configuration.GetSection(nameof(CacheOptions)).Bind(cacheOptions);
        _services.AddSingleton(cacheOptions);
        
        _services.AddMemoryCache();
        _services.AddDistributedMemoryCache();

        #endregion

        #region Integrations Urls

        var baseUrl = TestLocally ? "https://localhost:5001/" : _configuration.GetValue<string>("IntegrationUrl");

        TestClientId = _configuration.GetValue<string>("TestClientId");

        _services.AddHttpClient(ClientName, client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => new DefaultHttpClientHandler())
            .AddPolicyHandler(HttpExtensions.RetryPolicy(ClientName, 3));

        #endregion

        ServiceProvider = _services.BuildServiceProvider();
            
        _clientFactory = ServiceProvider.GetRequiredService<IHttpClientFactory>();
    }

    protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, string jwtToken, string clientName,
        int timeout = 10)
    {
        var client = _clientFactory.CreateClient(clientName);
        client.Timeout = TimeSpan.FromMinutes(timeout);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        var response = await client.SendAsync(requestMessage);
        return response;
    }

    protected static async Task<T> GetResponseAsync<T>(HttpResponseMessage response)
    {
        var resultString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(resultString);
    }
}