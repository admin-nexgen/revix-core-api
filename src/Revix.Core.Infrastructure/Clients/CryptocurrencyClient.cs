using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Revix.Core.Common.Constants;
using Revix.Core.Domain.Clients;
using Revix.Core.Domain.Models;

namespace Revix.Core.Infrastructure.Clients;

public class CryptocurrencyClient : ICryptocurrencyClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptocurrencyClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ListingsLatestResponse> GetListingsLatestAsync(ListingsLatestRequest request)
    {
        using var client = _httpClientFactory.CreateClient(nameof(CryptocurrencyClient));
        
        var query = new Dictionary<string, string>();

        if (request.Start >= 1)
            query["start"] = request.Start.ToString();
        if (request.Limit is >= 1 and <= 5000)
            query["limit"] = request.Limit.ToString();            
        if (request.PriceMin is >= 0 and <= 100000000000000000)
            query["price_min"] = request.PriceMin.ToString();
        if (request.PriceMax is >= 0 and <= 100000000000000000)
            query["price_max"] = request.PriceMax.ToString();
        if (request.MarketCapMin is >= 0 and <= 100000000000000000)
            query["market_cap_min"] = request.MarketCapMin.ToString();
        if (request.MarketCapMax is >= 0 and <= 100000000000000000)
            query["market_cap_max"] = request.MarketCapMax.ToString();
        if (request.Volume24hMin is >= 0 and <= 100000000000000000)
            query["volume_24h_min"] = request.Volume24hMin.ToString();
        if (request.Volume24hMax is >= 0 and <= 100000000000000000)
            query["volume_24h_max"] = request.Volume24hMax.ToString();
        if (request.CirculatingSupplyMin is >= 0 and <= 100000000000000000)
            query["circulating_supply_min"] = request.CirculatingSupplyMin.ToString();
        if (request.CirculatingSupplyMax is >= 0 and <= 100000000000000000)
            query["circulating_supply_max"] = request.CirculatingSupplyMax.ToString();
        if (request.PercentageChange24hMin is >= 0 and <= 100)
            query["percent_change_24h_min"] = request.PercentageChange24hMin.ToString();
        if (request.PercentageChange24hMax is >= 0 and <= 100)
            query["percent_change_24h_max"] = request.PercentageChange24hMax.ToString();
        if (!string.IsNullOrWhiteSpace(request.Convert))
            query["convert"] = request.Convert;
        if (!string.IsNullOrWhiteSpace(request.ConvertId))
            query["convert_id"] = request.ConvertId;
        if (!string.IsNullOrWhiteSpace(request.Sort))
            query["sort"] = request.Sort;
        if (!string.IsNullOrWhiteSpace(request.SortDir))
            query["sort_dir"] = request.SortDir;
        if (!string.IsNullOrWhiteSpace(request.CryptoCurrencyType))
            query["cryptocurrency_type"] = request.CryptoCurrencyType;
        if (!string.IsNullOrWhiteSpace(request.Tag))
            query["tag"] = request.Tag;
        if (!string.IsNullOrWhiteSpace(request.Aux))
            query["aux"] = request.Aux;

        var url = QueryHelpers.AddQueryString(client.BaseAddress + "v1/cryptocurrency/listings/latest", query);
        
        var httpRequestMessage  = new HttpRequestMessage(HttpMethod.Get, url);
        httpRequestMessage.Headers.Add(CryptocurrencyConstants.ApiKeyName, CryptocurrencyConstants.ApiKeyValue);

        var httpResponseMessage = await client.SendAsync(httpRequestMessage);
        
        var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<ListingsLatestResponse>(jsonResponse);
        
        return response;
    }
}