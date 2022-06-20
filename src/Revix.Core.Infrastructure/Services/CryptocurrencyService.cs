using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Revix.Core.Domain.Clients;
using Revix.Core.Domain.Models;
using Revix.Core.Domain.Repositories;
using Revix.Core.Domain.Services;

namespace Revix.Core.Infrastructure.Services;

public class CrytocurrencyService : ICryptocurrencyService
{
    private readonly IMapper _mapper;
    
    private readonly ICryptocurrencyClient _cryptocurrencyClient;
    
    private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

    public CrytocurrencyService(IMapper mapper, ICryptocurrencyClient cryptocurrencyClient, ICryptocurrencyRepository cryptocurrencyRepository)
    {
        _mapper = mapper;
        _cryptocurrencyClient = cryptocurrencyClient;
        _cryptocurrencyRepository = cryptocurrencyRepository;
    }

    public async Task<ListingsLatestResponse> GetListingsLatestAsync(ListingsLatestRequest request)
    {
        var message = _mapper.Map<ListingsLatestRequest>(request);
        
        var response = await _cryptocurrencyClient.GetListingsLatestAsync(message);

        return _mapper.Map<ListingsLatestResponse>(response);
    }

    public async Task SaveCryptocurrenciesAsync(IEnumerable<Cryptocurrency> cryptoCurrencies)
    {
        await _cryptocurrencyRepository.SaveCryptoCurrenciesAsync(cryptoCurrencies);
    }
}