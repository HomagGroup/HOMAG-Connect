using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.ProductionProtocol;
using Newtonsoft.Json;
using Shouldly;
using System.Globalization;

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
        var workstations = await productionManagerClient.GetWorkstations();

        Assert.IsNotNull(workstations);

        var protocolList = new List<ProcessedItem>();
        foreach (var workstation in workstations)
        {
            var protocolTask = productionManagerClient.GetProductionProtocol(workstation.Id.ToString(), take:10, skip:0, daysBack:7);
            var response = await protocolTask ?? Array.Empty<ProcessedItem>();
            protocolList.AddRange(response);
        }

        protocolList.Trace();

        Assert.IsNotNull(protocolList);

        TestContext?.AddResultFile(protocolList.TraceToFile(nameof(ProductionProtocol_Trace)).FullName);
    }

    /// <summary />
    [TestMethod]
    public async Task ProductionProtocol_TraceLocalized()
    {
        var productionManagerClient = GetProductionManagerClient();
        var workstations = await productionManagerClient.GetWorkstations();
        var protocolList = new List<ProcessedItem>();

        Assert.IsNotNull(workstations);

        foreach (var workstation in workstations)
        {
            var protocolTask = productionManagerClient.GetProductionProtocol(workstation.Id.ToString(), take:10, skip:0, daysBack:7);
            var response = await protocolTask ?? Array.Empty<ProcessedItem>();
            protocolList.AddRange(response);
        }

        protocolList.ShouldNotBeNull();

        var culture = CultureInfo.GetCultureInfo("de-DE");

        var serializedObjectLocalized = protocolList.SerializeLocalized(culture);

        var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

        dynamic.ShouldNotBeNull();

        TestContext?.AddResultFile(dynamic.TraceToFile(nameof(ProductionProtocol_TraceLocalized)).FullName);
    }
}