using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.ProductionProtocol;
using Newtonsoft.Json;
using Shouldly;
using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.QueryFilter;
using HomagConnect.OrderManager.Contracts.Import;
using HomagConnect.ProductionManager.Samples.ProductionProtocol.Actions;

namespace HomagConnect.ProductionManager.Tests.ProductionProtocol;

/// <summary />
[TestClass]
[IntegrationTest("ProductionManager.ProductionProtocol")]
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
            var protocolTask = productionManagerClient.GetProductionProtocol(workstation.Id.ToString(), take: 10, skip: 0, daysBack: 70, OutputFormat.Default, filter: "description eq 'Test'");
            var response = await protocolTask ?? Array.Empty<ProcessedItem>();
            protocolList.AddRange(response);
        }

        protocolList.Trace();

        Assert.IsNotNull(protocolList);

        TestContext?.AddResultFile(protocolList.TraceToFile(nameof(ProductionProtocol_Trace)).FullName);
    }

    /// <summary />
    [TestMethod]
    public async Task ProductionProtocol_TraceWithQuery()
    {
        var productionManagerClient = GetProductionManagerClient();
        var workstations = await productionManagerClient.GetWorkstations();

        Assert.IsNotNull(workstations);

        var fr = new FilterRequest();
        fr.AddEquals("description", "Test");
        var or = new OrderByRequest();
        or.Fields.Add(new OrderByField() { Column = "description", Direction = OrderByDirection.Ascending });

        var protocolList = new List<ProcessedItem>();
        foreach (var workstation in workstations)
        {
            var protocolTask = productionManagerClient.GetProductionProtocol(workstation.Id.ToString(), take: 10, skip: 0, daysBack: 7, 
                filterRequest: fr, orderByRequest: or);
            var response = await protocolTask ?? Array.Empty<ProcessedItem>();
            protocolList.AddRange(response);
        }

        protocolList.Trace();

        Assert.IsNotNull(protocolList);

        TestContext?.AddResultFile(protocolList.TraceToFile(nameof(ProductionProtocol_Trace)).FullName);
    }
    /// <summary />
    [TestMethod]
    public async Task ProductionProtocol_Sample_GetProductionProtocolFilterObject()
    {
        var productionManagerClient = GetProductionManagerClient();

        await GetProductionProtocolSamples.GetProductionProtocolFilterObject(productionManagerClient);
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
            var protocolTask = productionManagerClient.GetProductionProtocol(workstation.Id.ToString(), take:10, filter: null);
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
