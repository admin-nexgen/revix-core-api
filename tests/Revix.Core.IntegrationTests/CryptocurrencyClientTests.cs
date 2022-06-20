using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Revix.Core.Domain.Clients;
using Revix.Core.Domain.Models;
using Revix.Core.Infrastructure.Common.Testing;

namespace Revix.Core.IntegrationTests;

[TestClass]
public class CryptocurrencyClientTests : IntegrationTestBase
{
    [TestMethod]
    public async Task GetLatestListingsTest()
    {
        var agent = ServiceProvider.GetRequiredService<ICryptocurrencyClient>();
        var listing = await agent.GetListingsLatestAsync(new ListingsLatestRequest());
        Assert.IsNotNull(listing);
        Assert.IsNotNull(listing.Data);
        Assert.IsNotNull(listing.Status);
        Assert.IsTrue(listing.Data.Count > 0);
    }
}