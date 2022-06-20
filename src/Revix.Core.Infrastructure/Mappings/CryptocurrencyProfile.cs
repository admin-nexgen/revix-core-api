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
            .ForMember(dst => dst.CryptocurrencyId, opt => opt.Ignore())
            .EqualityComparison((x, y) => x.Id > 0 && x.Id == y.Id)
            .ReverseMap();

        CreateMap<Quote, Core.Domain.Entities.Quote>()
            .ForMember(dst => dst.QuoteId, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Currency, Core.Domain.Entities.Currency>()
            .ForMember(dst => dst.CurrencyId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Platform, Core.Domain.Entities.Platform>()
            .ForMember(dst => dst.PlatformId, opt => opt.Ignore())
            .EqualityComparison((x, y) => x.Id > 0 && x.Id == y.Id)
            .ReverseMap();
    }
}