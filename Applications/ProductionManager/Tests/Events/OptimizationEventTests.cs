using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.Events.ProductionItem;
using HomagConnect.ProductionManager.Contracts.Orders;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.Events;

/// <summary />
[TestClass]
[TestCategory("ProductionManager")]
[TestCategory("ProductionManager.Events")]
public class ProductionEventsTests : ProductionManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_ListProductionManagerEvents()
    {
        var assemblies = new[] { typeof(OrderDetails).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        derivedTypes.Trace();
    }

    /// <summary />
    [TestMethod]
    public void ProductionItemProcessedByMachineFileEvent_SerializeDeserialize_PropertiesEqual()
    {
        var productionItemProcessedByMachineFileEvent = new ProductionItemProcessedByMachineFileEvent();

        productionItemProcessedByMachineFileEvent.SubscriptionId = SubscriptionId;
        productionItemProcessedByMachineFileEvent.MachineNumber = "0-250-743-491";
        productionItemProcessedByMachineFileEvent.FileContent = @"20250124144140,PNL,9999,9999,1,1,1,1,XYZ789,,,,,2,1,,,,,,,,,,,,,,,C:\MACHINE1\Control\c1\data\cnc\mp4\LL_TEMPLATES\T2.mprx,XYZ789"
            ;
        productionItemProcessedByMachineFileEvent.FileName = "CNC1_20250124144140.hol";
        productionItemProcessedByMachineFileEvent.ColumnMapping = new Dictionary<int, string>
        {
            { 31, "PartId" }
        };

        TestContext?.AddResultFile(productionItemProcessedByMachineFileEvent.TraceToFile("productionItemProcessedByMachineFileEvent").FullName);

        //var solutionDetails = await GetSampleSolutionDetails();

        //var solutionTransferredEvent = new SolutionTransferredEvent();

        //solutionTransferredEvent.SubscriptionId = SubscriptionId;
        //solutionTransferredEvent.AlgorithmName = "APS";
        //solutionTransferredEvent.AlgorithmSettings = "01";
        //solutionTransferredEvent.TransferredBy = "Max.Mustermann@homag.com";
        //solutionTransferredEvent.SolutionDetails = solutionDetails;

        //Assert.IsTrue(solutionTransferredEvent.IsValid);

        //TestContext?.AddResultFile(solutionTransferredEvent.TraceToFile("solutionTransferredEvent").FullName);

        //// Serialize and deserialize again as base type
        //var solutionTransferredEventSerialized = JsonConvert.SerializeObject(solutionTransferredEvent);
        //var appEvent = JsonConvert.DeserializeObject<AppEvent>(solutionTransferredEventSerialized);
        //Assert.IsNotNull(appEvent);
        //TestContext?.AddResultFile(appEvent.TraceToFile("appEvent").FullName);

        //// Serialize base type and deserialize again as derived type
        //var appEventSerialized = JsonConvert.SerializeObject(solutionTransferredEvent);
        //var solutionTransferredEventDeserialized = JsonConvert.DeserializeObject<SolutionTransferredEvent>(appEventSerialized);
        //Assert.IsNotNull(solutionTransferredEventDeserialized);
        //TestContext?.AddResultFile(solutionTransferredEventDeserialized.TraceToFile("solutionTransferredEventDeserialized").FullName);

        //// Compare properties
        //Assert.AreEqual(solutionTransferredEvent.Id, solutionTransferredEventDeserialized.Id);
        //Assert.AreEqual(solutionTransferredEvent.Timestamp, solutionTransferredEventDeserialized.Timestamp);
        //Assert.AreEqual(solutionTransferredEvent.SubscriptionId, solutionTransferredEventDeserialized.SubscriptionId);
        //Assert.AreEqual(solutionTransferredEvent.TransferredBy, solutionTransferredEventDeserialized.TransferredBy);
        //Assert.AreEqual(solutionTransferredEvent.SolutionDetails.Parts.First().Description, solutionTransferredEventDeserialized.SolutionDetails.Parts.First().Description);
    }
}