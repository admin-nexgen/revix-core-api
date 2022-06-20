using System.Collections.Generic;
using System.Threading.Tasks;
using Revix.Core.Domain.Models;

namespace Revix.Core.Domain.Services;

public interface ICryptocurrencyService
{
    Task<ListingsLatestResponse> GetListingsLatestAsync(ListingsLatestRequest request);

    Task SaveCryptocurrenciesAsync(IEnumerable<Cryptocurrency> cryptoCurrencies);
}