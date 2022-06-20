using System.Collections.Generic;
using Revix.Core.Domain.Models;

namespace Revix.Core.Application.Responses;

public class ListingsLatestResponse
{
    public Status Status { get; set; }
    
    public List<Cryptocurrency> Data { get; set; }
}