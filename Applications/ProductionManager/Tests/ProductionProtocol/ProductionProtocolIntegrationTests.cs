using System.Globalization;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.ProductionProtocol;

/// <summary />
[TestClass]
[IntegrationTest("ProductionManager.ProductionProtocol")]
[TemporaryDisabledOnServer(2026,2,28, "DF-Insights")]
public class ProductionProtocolIntegrationTests : ProductionManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task ProductionProtocol_Trace()
    {
        var productionManagerClient = GetProductionManagerClient();

        var productionProtocol = (await productionManagerClient.GetProductionProtocol("8892b810-ac7a-468f-9153-c1a4d6536463").ToListAsync()).OrderByDescending(p => p.Timestamp);

        Assert.IsNotNull(productionProtocol);

        TestContext?.AddResultFile(productionProtocol.TraceToFile(nameof(ProductionProtocol_Trace)).FullName);
    }

    /// <summary />
    [TestMethod]
    public async Task ProductionProtocol_TraceLocalized()
    {
        var productionManagerClient = GetProductionManagerClient();

        var productionProtocol = await productionManagerClient.GetProductionProtocol("8892b810-ac7a-468f-9153-c1a4d6536463");

        productionProtocol.ShouldNotBeNull();

        var culture = CultureInfo.GetCultureInfo("de-DE");

        var serializedObjectLocalized = productionProtocol.SerializeLocalized(culture);

        var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

        dynamic.ShouldNotBeNull();

        TestContext?.AddResultFile(dynamic.TraceToFile(nameof(ProductionProtocol_TraceLocalized)).FullName);
    }
}