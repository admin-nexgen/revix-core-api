using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Revix.Core.Application.Events;
using Revix.Core.Application.Requests;
using Revix.Core.Application.Responses;
using Revix.Core.Domain.Services;

namespace Revix.Core.Application.Handlers;

public class GetListingsLatestRequestHandler : IRequestHandler<GetListingsLatestRequest, ListingsLatestResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICryptocurrencyService _cryptocurrencyService;

    public GetListingsLatestRequestHandler(IMapper mapper, IMediator mediator, ICryptocurrencyService cryptocurrencyService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _cryptocurrencyService = cryptocurrencyService;
    }

    public async Task<ListingsLatestResponse> Handle(GetListingsLatestRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<Domain.Models.ListingsLatestRequest>(request);
        
        var result = await _cryptocurrencyService.GetListingsLatestAsync(query);

        var response = new ListingsLatestResponse
        {
            Data = result.Data,
            Status = result.Status
        };

        var @event = new GetListingsLatestEvent
        {
            Data = response.Data,
            Status = result.Status
        };

        await _mediator.Publish(@event, cancellationToken);

        return response;
    }
}