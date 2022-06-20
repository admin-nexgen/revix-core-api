using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Revix.Core.Application.Common.Interfaces;
using Revix.Core.Domain.Repositories;
using Cryptocurrency = Revix.Core.Domain.Models.Cryptocurrency;

namespace Revix.Core.Infrastructure.Repositories;

public class CryptocurrencyRepository : ICryptocurrencyRepository
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public CryptocurrencyRepository(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task SaveCryptoCurrenciesAsync(IEnumerable<Cryptocurrency> cryptocurrencies)
    {
        foreach (var cryptocurrency in cryptocurrencies)
        {
            await _dbContext.Cryptocurrencies.Persist(_mapper).InsertOrUpdateAsync(cryptocurrency);
        }

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}