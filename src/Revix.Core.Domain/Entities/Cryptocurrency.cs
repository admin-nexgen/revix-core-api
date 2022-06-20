using System;
using System.ComponentModel.DataAnnotations;

namespace Revix.Core.Domain.Entities;

public class Cryptocurrency
{
    public Cryptocurrency()
    {
        Tags = Array.Empty<string>();
    }
    
    [Key]
    public Guid CryptocurrencyId { get; set; }
    
    public long Id { get; set; }

    public string Name { get; set; }

    public string Symbol { get; set; }

    public string Slug { get; set; }

    public decimal? CmcRank { get; set; }

    public decimal? NumMarketPairs { get; set; }

    public decimal? CirculatingSupply { get; set; }

    public decimal? TotalSupply { get; set; }

    public decimal? MaxSupply { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? DateAdded { get; set; }
    
    public string SelfReportedCirculatingSupply { get; set; }

    public string SelfReportedMarketCap { get; set; }

    public virtual string[] Tags { get; set; }
    
    public virtual Quote Quote { get; set; }
    
    public virtual Platform Platform { get; set; }
}