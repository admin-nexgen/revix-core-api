using System;
using System.ComponentModel.DataAnnotations;

namespace Revix.Core.Domain.Entities;

public class Currency
{
    [Key]
    public Guid CurrencyId { get; set; }
    
    public decimal? Price { get; set; }

    public decimal? Volume24h { get; set; }

    public decimal? VolumeChange24h { get; set; }

    public decimal? PercentChange1h { get; set; }

    public decimal? PercentChange24h { get; set; }

    public decimal? PercentChange7d { get; set; }

    public decimal? MarketCap { get; set; }

    public decimal? MarketCapDominance { get; set; }

    public decimal? FullyDilutedMarketCap { get; set; }

    public DateTime? LastUpdated { get; set; }
}