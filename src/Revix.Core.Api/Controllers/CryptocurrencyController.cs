using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revix.Core.Application.Requests;
using Revix.Core.Application.Responses;
using Revix.Core.Infrastructure.Common.Controllers;

namespace Revix.Core.Api.Controllers;

[ApiVersionNeutral]
public class CryptocurrencyController : ApiControllerBase
{
    /// <summary>
    /// Returns a paginated list of all active cryptocurrencies with latest market data.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("listings/latest")]
    public async Task<ActionResult<ListingsLatestResponse>> GetListingsLatest([FromQuery]GetListingsLatestRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}