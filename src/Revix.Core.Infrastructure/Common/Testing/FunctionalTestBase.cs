using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Revix.Core.Common.Extensions;
using Revix.Core.Common.HttpClients;
using Revix.Core.Infrastructure.Clients;

namespace Revix.Core.Infrastructure.Common.Testing;

/// <summary>
/// Base class for end to end tests ie. Tests where we restfully hit the endpoint interrogating response codes
/// </summary>
public class FunctionalTestBase : TestBase
{
    private readonly IConfiguration _configuration;
    private readonly IServiceCollection _services = new ServiceCollection();
    private readonly IHttpClientFactory _clientFactory;
        
    protected string BaseUrl { get; }
    
    protected ServiceProvider ServiceProvider { get; }

    protected FunctionalTestBase()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        _configuration = builder.Build();
            
        _services.AddHttpContextAccessor();
        
        #region  Cache

        _services.AddMemoryCache();
        _services.AddDistributedMemoryCache();

        #endregion

        #region Integrations Urls

        var baseUrl = _configuration.GetValue<string>("CoinMarketCapApiUrl");
        BaseUrl = baseUrl;

        _services.AddHttpClient(nameof(CryptocurrencyClient), client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "https://sandbox-api.coinmarketcap.com");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new DefaultHttpClientHandler())
            .AddPolicyHandler(HttpExtensions.RetryPolicy(nameof(CryptocurrencyClient), 3));

        #endregion

        ServiceProvider = _services.BuildServiceProvider();
            
        _clientFactory = ServiceProvider.GetRequiredService<IHttpClientFactory>();
    }

    protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, 
        int timeout = 10)
    {
        var client = _clientFactory.CreateClient(nameof(CryptocurrencyClient));
        client.Timeout = TimeSpan.FromMinutes(timeout);

        var response = await client.SendAsync(requestMessage);
        return response;
    }

    protected static async Task<T> GetResponseAsync<T>(HttpResponseMessage response)
    {
        var resultString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(resultString);
    }
}