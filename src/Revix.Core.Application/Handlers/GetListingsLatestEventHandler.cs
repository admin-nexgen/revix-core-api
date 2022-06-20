using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Revix.Core.Application.Events;
using Revix.Core.Domain.Services;

namespace Revix.Core.Application.Handlers;

public class GetListingsLatestEventHandler : INotificationHandler<GetListingsLatestEvent>
{
    private readonly ICryptocurrencyService _cryptocurrencyService;

    public GetListingsLatestEventHandler(ICryptocurrencyService cryptocurrencyService)
    {
        _cryptocurrencyService = cryptocurrencyService;
    }

    public async Task Handle(GetListingsLatestEvent notification, CancellationToken cancellationToken)
    {
        await _cryptocurrencyService.SaveCryptocurrenciesAsync(notification.Data);
    }
}