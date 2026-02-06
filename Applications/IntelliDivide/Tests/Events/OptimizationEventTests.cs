using Shouldly;
using System.Globalization;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Events;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Tests.Base;
using Newtonsoft.Json;
using HomagConnect.Base.Contracts;

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
        var assemblies = new[] { typeof(OptimizationRequest).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        derivedTypes.ShouldNotBeNull();
        derivedTypes.Length.ShouldBeGreaterThan(0);

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
        solutionTransferredEvent.OptimizationName = "Optimization 01";
        solutionTransferredEvent.AlgorithmSettings = "01";
        solutionTransferredEvent.TransferredBy = "Test.SolutionTransferred@homag.com";
        solutionTransferredEvent.MachineDisplayName = "My Sawteq 0101";
        solutionTransferredEvent.MachineType = MachineType.Cutting;
        solutionTransferredEvent.SolutionExportUri = new Uri("https://www.google.com");
        solutionTransferredEvent.SolutionDetails = solutionDetails;

        solutionTransferredEvent.IsValid.ShouldBeTrue("The given event should be valid.");

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
        solutionTransferredEventDeserialized!.Id.ShouldBe(solutionTransferredEvent.Id);
        solutionTransferredEventDeserialized.Timestamp.ShouldBe(solutionTransferredEvent.Timestamp);
        solutionTransferredEventDeserialized.SubscriptionId.ShouldBe(solutionTransferredEvent.SubscriptionId);
        solutionTransferredEventDeserialized.TransferredBy.ShouldBe(solutionTransferredEvent.TransferredBy);
        solutionTransferredEventDeserialized.SolutionDetails.Parts.First().Description.ShouldBe(solutionTransferredEvent.SolutionDetails.Parts.First().Description);
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

        solutions.ShouldNotBeNull();
        solutions!.Count.ShouldBeGreaterThan(0);

        var solution = solutions!.First();

        var solutionDetails = await intelliDivide.GetSolutionDetails(optimization.Id, solution.Id);

        solutionDetails.ShouldNotBeNull();
        return solutionDetails;
    }
}