using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Revix.Core.Infrastructure.Mappings;

namespace Revix.Core.UnitTests.Mappings;

[TestClass]
public class MappingTests
{
    [TestMethod]
    public void Check_AllMappingConfigurations_ShouldBeValid()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(CryptocurrencyProfile).Assembly);
        });

        configuration.AssertConfigurationIsValid();
    }
}