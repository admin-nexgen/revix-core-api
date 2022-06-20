using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Revix.Core.Domain.Models;
using Revix.Core.Infrastructure.Common.Testing;

namespace Revix.Core.FunctionalTests;

[TestClass]
public class CryptocurrencyControllerTests : FunctionalTestBase
{
    [TestMethod]
    public async Task GetLatestListingsTest()
    {
        const string path = "/v1/listings/latest?start=1&limit=100";
        var url = BaseUrl + path;

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await SendAsync(request);
            
        var result = await GetResponseAsync<ListingsLatestResponse>(response);

        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}