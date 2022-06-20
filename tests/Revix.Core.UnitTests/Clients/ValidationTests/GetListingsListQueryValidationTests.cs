using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Revix.Core.Application.Common.Exceptions;
using Revix.Core.Application.Requests;

namespace Revix.Core.UnitTests.Clients.ValidationTests;

[TestClass]
public class ListingsListQueryValidationTests : ValidationTestFixture
{
    [TestMethod]
    public async Task GetClientInfoQuery_WhereCompanyProfileIdIsZero_ReturnsException()
    {
        var query = new GetListingsLatestRequest();

        await Assert.ThrowsExceptionAsync<ValidationException>(() => SendAsync(query));
    }
}