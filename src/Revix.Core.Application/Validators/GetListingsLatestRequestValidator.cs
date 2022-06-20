using FluentValidation;
using Revix.Core.Application.Requests;

namespace Revix.Core.Application.Validators;

public class GetListingsLatestRequestValidator : AbstractValidator<GetListingsLatestRequest>
{
    public GetListingsLatestRequestValidator()
    {
    }
}