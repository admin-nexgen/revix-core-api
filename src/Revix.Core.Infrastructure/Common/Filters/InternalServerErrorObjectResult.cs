using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Revix.Core.Infrastructure.Common.Filters;

public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object error)
        : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}