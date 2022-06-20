using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Revix.Core.Domain.Models;
using Revix.Core.Infrastructure.Clients;
using Revix.Core.Infrastructure.Common.Testing;

namespace Revix.Core.IntegrationTests;

[TestClass]
public class CryptocurrencyClientTests : IntegrationTestBase
{
    private IMapper _mapper;
    private ILogger<CryptocurrencyClient> _logger;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = ServiceProvider.GetRequiredService<IMapper>();
        _logger = ServiceProvider.GetRequiredService<ILogger<CryptocurrencyClient>>();
    }

    [TestMethod]
    public async Task GetClientById_WhereValidIdSupplied_ReturnsValidClient()
    {
        var agent = ServiceProvider.GetRequiredService<CryptocurrencyClient>();
        var clientInfo = await agent.GetListingsLatestAsync(new ListingsLatestRequest());
        Assert.IsNotNull(clientInfo);
        Assert.IsNotNull(clientInfo.Data);
        Assert.IsNotNull(clientInfo.Status);
        Assert.IsTrue(clientInfo.Data.Count > 0);
    }
}