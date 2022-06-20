using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Revix.Core.Domain.Models;

public class Cryptocurrency
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("slug")]
    public string Slug { get; set; }

    [JsonProperty("cmc_rank")]
    public decimal? CmcRank { get; set; }

    [JsonProperty("num_market_pairs")]
    public decimal? NumMarketPairs { get; set; }

    [JsonProperty("circulating_supply")]
    public decimal? CirculatingSupply { get; set; }

    [JsonProperty("total_supply")]
    public decimal? TotalSupply { get; set; }

    [JsonProperty("max_supply")]
    public decimal? MaxSupply { get; set; }

    [JsonProperty("last_updated")]
    public DateTime? LastUpdated { get; set; }

    [JsonProperty("date_added")]
    public DateTime? DateAdded { get; set; }

    [JsonProperty("tags")]
    public string[] Tags { get; set; }

    [JsonProperty("platform")]
    public Platform Platform { get; set; }

    [JsonProperty("self_reported_circulating_supply")]
    public string SelfReportedCirculatingSupply { get; set; }

    [JsonProperty("self_reported_market_cap")]
    public string SelfReportedMarketCap { get; set; }

    [JsonProperty("quote")]
    public Quote Quote { get; set; }
}

public class Quote
{
    [JsonProperty("USD")]
    public Currency USD { get; set; }

    [JsonProperty("BTC")]
    public Currency BTC { get; set; }
}

public class ListingsLatestResponse
{
    [JsonProperty("status")]
    public Status Status { get; set; }

    [JsonProperty("data")]
    public List<Cryptocurrency> Data { get; set; }
}

public class Status
{
    [JsonProperty("timestamp")]
    public DateTime? Timestamp { get; set; }

    [JsonProperty("error_code")]
    public int? ErrorCode { get; set; }

    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }

    [JsonProperty("elapsed")]
    public int? Elapsed { get; set; }

    [JsonProperty("credit_count")]
    public int? CreditCount { get; set; }

    [JsonProperty("notice")]
    public string Notice { get; set; }
}

public class Currency
{
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("volume_24h")]
    public decimal? Volume24h { get; set; }

    [JsonProperty("volume_change_24h")]
    public decimal? VolumeChange24h { get; set; }

    [JsonProperty("percent_change_1h")]
    public decimal? PercentChange1h { get; set; }

    [JsonProperty("percent_change_24h")]
    public decimal? PercentChange24h { get; set; }

    [JsonProperty("percent_change_7d")]
    public decimal? PercentChange7d { get; set; }

    [JsonProperty("market_cap")]
    public decimal? MarketCap { get; set; }

    [JsonProperty("market_cap_dominance")]
    public decimal? MarketCapDominance { get; set; }

    [JsonProperty("fully_diluted_market_cap")]
    public decimal? FullyDilutedMarketCap { get; set; }

    [JsonProperty("last_updated")]
    public DateTime? LastUpdated { get; set; }
}

public class Platform
{   
    [JsonProperty("id")]
    public long Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("symbol")]
    public string Symbol { get; set; }
    
    [JsonProperty("slug")]
    public string Slug { get; set; }
    
    [JsonProperty("token_address")]
    public string TokenAddress { get; set; }
}