using FluentAssertions;

using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Events;
using HomagConnect.IntelliDivide.Contracts.Request;
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
        var assemblies = new[] { typeof(OptimizationRequest).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        derivedTypes.Should().NotBeNullOrEmpty();

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

        solutionTransferredEvent.IsValid.Should().BeTrue("The given event should be valid.");

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
        solutionTransferredEventDeserialized.Id.Should().Be(solutionTransferredEvent.Id, "That means the deserialization was successful for solution id.");
        solutionTransferredEventDeserialized.Timestamp.Should().Be(solutionTransferredEvent.Timestamp, "That means the deserialization was successful for solution.");
        solutionTransferredEventDeserialized.SubscriptionId.Should().Be(solutionTransferredEvent.SubscriptionId, "That means the deserialization was successful for solution.");
        solutionTransferredEventDeserialized.TransferredBy.Should().Be(solutionTransferredEvent.TransferredBy, "That means the deserialization was successful for solution.");
        solutionTransferredEventDeserialized.SolutionDetails.Parts.First().Description.Should().Be(solutionTransferredEvent.SolutionDetails.Parts.First().Description, "That means the deserialization was successful for solution.");
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

        solutions.Should().NotBeNull($"Solutions should not be null for {optimization.Id}.");

        var solution = solutions.First();

        var solutionDetails = await intelliDivide.GetSolutionDetails(optimization.Id, solution.Id);

        solutionDetails.Should().NotBeNull($"Solutions for {optimization.Id} should contain at least one element.");
        return solutionDetails;
    }
}