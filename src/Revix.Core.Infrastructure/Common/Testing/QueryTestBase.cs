using System;
using AutoMapper;
using Revix.Core.Infrastructure.Mappings;
using Revix.Core.Infrastructure.Persistence;

namespace Revix.Core.Infrastructure.Common.Testing;

public class QueryTestBase : TestBase, IDisposable
{
    protected ApplicationDbContext Context { get; }
    protected IMapper Mapper { get; }

    protected QueryTestBase()
    {
        Context = InMemoryDbContextFactory.Create();

        var configurationProvider = new MapperConfiguration(cfg =>
        {             
            cfg.AddMaps(typeof(CryptocurrencyProfile).Assembly);
        });
            
        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        InMemoryDbContextFactory.Destroy(Context);
    }
}