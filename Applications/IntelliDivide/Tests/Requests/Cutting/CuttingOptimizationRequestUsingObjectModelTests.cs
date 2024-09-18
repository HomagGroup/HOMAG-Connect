using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Cutting.ObjectModel;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
[TestCategory("IntelliDivide.Requests.Cutting.ObjectModel")]
public class CuttingOptimizationRequestUsingObjectModelTests : IntelliDivideTestBase
{
    /// <summary />
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Cutting, CommonSampleSettings.TimeoutDuration);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_RequiredProperties_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_RequiredProperties_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_AdditionalProperties_Optimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_AdditionalProperties_Optimize(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_TypicalProperties_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_TypicalProperties_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_TypicalProperties_ImportAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_RequiredProperties_ImportAndOptimize(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_TypicalProperties_ImportOptimizeAndSend()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_RequiredProperties_ImportOptimizeAndSend(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_TypicalProperties_ImportValidateAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_RequiredProperties_ImportValidateAndOptimize(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task CuttingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly(intelliDivide);
    }
}

#pragma warning restore S2699 // Tests should include assertions