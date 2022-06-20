using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Revix.Core.Application.Responses;

namespace Revix.Core.Application.Requests;

public class GetListingsLatestRequest : IRequest<ListingsLatestResponse>
{
    /// <summary>
    /// Optionally offset the start (1-based index) of the paginated list of items to return.
    /// </summary>
    [DefaultValue(1)]
    [FromQuery(Name = "start")]
    public int Start { get; set; }
        
    [DefaultValue(100)]
    [FromQuery(Name = "limit")]
    public int Limit { get; set; }
        
    [FromQuery(Name = "price_min")]
    public decimal? PriceMin { get; set; }
        
    [FromQuery(Name = "price_max")]
    public decimal? PriceMax { get; set; }
        
    [FromQuery(Name = "market_cap_min")]
    public decimal? MarketCapMin { get; set; }
        
    [FromQuery(Name = "market_cap_max")]
    public decimal? MarketCapMax { get; set; }
        
    [FromQuery(Name = "volume_24h_min")]
    public decimal? Volume24hMin { get; set; }
        
    [FromQuery(Name = "volume_24h_max")]
    public decimal? Volume24hMax { get; set; }
        
    [FromQuery(Name = "circulating_supply_min")]
    public decimal? CirculatingSupplyMin { get; set; }
        
    [FromQuery(Name = "circulating_supply_max")]
    public decimal? CirculatingSupplyMax { get; set; }
        
    [FromQuery(Name = "percentage_change_24h_min")]
    public int? PercentageChange24hMin { get; set; }
        
    [FromQuery(Name = "percentage_change_24h_max")]
    public int? PercentageChange24hMax { get; set; }
        
    [FromQuery(Name = "convert")]
    public string Convert { get; set; }
        
    [FromQuery(Name = "convert_id")]
    public string ConvertId { get; set; }
    
    [FromQuery(Name = "sort")]
    public string Sort { get; set; }
        
    [FromQuery(Name = "sort_dir")]
    public string SortDir { get; set; }
    
    [FromQuery(Name = "cryptocurrency_type")]
    public string CryptoCurrencyType { get; set; }
        
    [FromQuery(Name = "tag")]
    public string Tag { get; set; }
        
    [FromQuery(Name = "aux")]
    public string Aux { get; set; }
}