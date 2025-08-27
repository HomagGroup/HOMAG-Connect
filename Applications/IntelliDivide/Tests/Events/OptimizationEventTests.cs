using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Events;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Tests.Base;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Tests.Events;

/// <summary />
[TestClass]
[TestCategory("Events")]
[TestCategory("IntelliDivide.Events")]
public class OptimizationEventTests : IntelliDivideTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_ListIntelliDivideEvents()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        derivedTypes.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task SolutionTransferred_SerializeDeserialize_PropertiesEqual()
    {
        var solutionDetails = await GetSampleSolutionDetails();

        var solutionTransferredEvent = new SolutionTransferredEvent();

        solutionTransferredEvent.SubscriptionId = SubscriptionId;
        solutionTransferredEvent.AlgorithmName = "APS";
        solutionTransferredEvent.AlgorithmSettings = "01";
        solutionTransferredEvent.TransferredBy = "Max.Mustermann@homag.com";
        solutionTransferredEvent.SolutionDetails = solutionDetails;

        Assert.IsTrue(solutionTransferredEvent.IsValid);

        TestContext?.AddResultFile(solutionTransferredEvent.TraceToFile("solutionTransferredEvent").FullName);

        // Serialize and deserialize again as base type
        var solutionTransferredEventSerialized = JsonConvert.SerializeObject(solutionTransferredEvent, SerializerSettings.Default);
        var appEvent = JsonConvert.DeserializeObject<AppEvent>(solutionTransferredEventSerialized, SerializerSettings.Default);
        Assert.IsNotNull(appEvent);
        TestContext?.AddResultFile(appEvent.TraceToFile("appEvent").FullName);

        // Serialize base type and deserialize again as derived type
        var appEventSerialized = JsonConvert.SerializeObject(solutionTransferredEvent, SerializerSettings.Default);
        var solutionTransferredEventDeserialized = JsonConvert.DeserializeObject<SolutionTransferredEvent>(appEventSerialized, SerializerSettings.Default);
        Assert.IsNotNull(solutionTransferredEventDeserialized);
        TestContext?.AddResultFile(solutionTransferredEventDeserialized.TraceToFile("solutionTransferredEventDeserialized").FullName);

        // Compare properties
        Assert.AreEqual(solutionTransferredEvent.Id, solutionTransferredEventDeserialized.Id);
        Assert.AreEqual(solutionTransferredEvent.Timestamp, solutionTransferredEventDeserialized.Timestamp);
        Assert.AreEqual(solutionTransferredEvent.SubscriptionId, solutionTransferredEventDeserialized.SubscriptionId);
        Assert.AreEqual(solutionTransferredEvent.TransferredBy, solutionTransferredEventDeserialized.TransferredBy);
        Assert.AreEqual(solutionTransferredEvent.SolutionDetails.Parts.First().Description, solutionTransferredEventDeserialized.SolutionDetails.Parts.First().Description);
    }

    private async Task<SolutionDetails> GetSampleSolutionDetails()
    {
        var intelliDivide = GetIntelliDivideClient();

        var optimizations = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 1);

        if (optimizations == null)
        {
            Assert.Inconclusive("No optimizations found.");
        }

        var optimization = optimizations.FirstOrDefault();

        if (optimization == null)
        {
            Assert.Inconclusive("No optimizations found.");
        }

        var solutions = await intelliDivide.GetSolutions(optimization.Id).ToListAsync();

        Assert.IsNotNull(solutions);

        var solution = solutions.First();

        var solutionDetails = await intelliDivide.GetSolutionDetails(optimization.Id, solution.Id);

        Assert.IsNotNull(solutionDetails);
        return solutionDetails;
    }
}