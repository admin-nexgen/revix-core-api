using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Revix.Core.Application.Requests;
using Revix.Core.Domain.Models;

namespace Revix.Core.Infrastructure.Mappings;

public class CryptocurrencyProfile : Profile
{
    public CryptocurrencyProfile()
    {
        CreateMap<GetListingsLatestRequest, ListingsLatestRequest>()
            .ReverseMap();

        CreateMap<Cryptocurrency, Core.Domain.Entities.Cryptocurrency>()
            .EqualityComparison((x, y) => x.Id > 0 && x.Id == y.Id)
            .ReverseMap();

        CreateMap<Quote, Core.Domain.Entities.Quote>()
            .ReverseMap();
        
        CreateMap<Currency, Core.Domain.Entities.Currency>()
            .ReverseMap();

        CreateMap<Platform, Core.Domain.Entities.Platform>()
            .EqualityComparison((x, y) => x.Id > 0 && x.Id == y.Id)
            .ReverseMap();
    }
}