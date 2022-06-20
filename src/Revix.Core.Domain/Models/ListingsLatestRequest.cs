using Newtonsoft.Json;

namespace Revix.Core.Domain.Models;

public class ListingsLatestRequest
{
    [JsonProperty("start")]
    public int Start { get; set; } = 1;
        
    [JsonProperty("limit")]
    public int Limit { get; set; } = 100;
        
    [JsonProperty("price_min")]
    public decimal? PriceMin { get; set; }
        
    [JsonProperty("price_max")]
    public decimal? PriceMax { get; set; }
        
    [JsonProperty("market_cap_min")]
    public decimal? MarketCapMin { get; set; }
        
    [JsonProperty("market_cap_max")]
    public decimal? MarketCapMax { get; set; }
        
    [JsonProperty("volume_24h_min")]
    public decimal? Volume24hMin { get; set; }
        
    [JsonProperty("volume_24h_max")]
    public decimal? Volume24hMax { get; set; }
        
    [JsonProperty("circulating_supply_min")]
    public decimal? CirculatingSupplyMin { get; set; }
        
    [JsonProperty("circulating_supply_max")]
    public decimal? CirculatingSupplyMax { get; set; }
        
    [JsonProperty("percentage_change_24h_min")]
    public int? PercentageChange24hMin { get; set; }
        
    [JsonProperty("percentage_change_24h_max")]
    public int? PercentageChange24hMax { get; set; }
        
    [JsonProperty("convert")]
    public string Convert { get; set; }
        
    [JsonProperty("convert_id")]
    public string ConvertId { get; set; }
        
    [JsonProperty("sort")]
    public string Sort { get; set; }
        
    [JsonProperty("sort_dir")]
    public string SortDir { get; set; }
        
    [JsonProperty("cryptocurrency_type")]
    public string CryptoCurrencyType { get; set; }
        
    [JsonProperty("tag")]
    public string Tag { get; set; }
        
    [JsonProperty("aux")]
    public string Aux { get; set; }
}