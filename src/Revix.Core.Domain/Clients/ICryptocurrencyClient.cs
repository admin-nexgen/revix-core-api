using System.Threading.Tasks;
using Revix.Core.Domain.Models;

namespace Revix.Core.Domain.Clients;

public interface ICryptocurrencyClient
{
    Task<ListingsLatestResponse> GetListingsLatestAsync(ListingsLatestRequest request);
}