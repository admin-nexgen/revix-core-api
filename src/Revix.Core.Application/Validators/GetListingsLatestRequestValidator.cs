using FluentValidation;
using Revix.Core.Application.Requests;

namespace Revix.Core.Application.Validators;

public class GetListingsLatestRequestValidator : AbstractValidator<GetListingsLatestRequest>
{
    public GetListingsLatestRequestValidator()
    {
        RuleFor(a => a.Start).GreaterThanOrEqualTo(1);
        RuleFor(a => a.Limit).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5000);
    }
}