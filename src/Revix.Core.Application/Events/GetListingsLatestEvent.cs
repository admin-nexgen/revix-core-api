using System.Collections.Generic;
using MediatR;
using Revix.Core.Domain.Models;

namespace Revix.Core.Application.Events;

public class GetListingsLatestEvent : INotification
{
    public Status Status { get; set; }
    public List<Cryptocurrency> Data { get; set; }
}