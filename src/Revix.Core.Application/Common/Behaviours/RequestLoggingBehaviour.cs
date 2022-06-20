using Revix.Core.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Revix.Core.Application.Common.Behaviours;

public class RequestLoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestLoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.LogInformation("Revix.Core Request: {Name} {@UserId} {@Request}",
            name, _currentUserService.UserId, request);

        return Task.CompletedTask;
    }
}