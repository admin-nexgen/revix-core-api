using System;
using System.ComponentModel.DataAnnotations;

namespace Revix.Core.Domain.Entities;

public class Quote
{
    public Quote()
    {
        USD = new Currency();
        BTC = new Currency();
    }

    [Key]
    public Guid QuoteId { get; set; }
    
    public virtual Currency USD { get; set; }

    public virtual Currency BTC { get; set; }
}