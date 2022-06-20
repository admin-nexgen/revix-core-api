using Revix.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Revix.Core.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
    
    DbSet<Currency> Currencies { get; set; }
    
    DbSet<Quote> Quotes { get; set; }   
    
    DbSet<Platform> Platforms { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}