using System.Collections.Generic;
using System.Threading.Tasks;
using Revix.Core.Domain.Models;

namespace Revix.Core.Domain.Repositories;

public interface ICryptocurrencyRepository
{
    Task SaveCryptoCurrenciesAsync(IEnumerable<Cryptocurrency> cryptocurrencies);
}