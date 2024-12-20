using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting.ObjectModel;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

#pragma warning disable S2699 // Tests should include assertions

/// <summary>
/// The class contains tests to ensure that the cutting optimization requests samples work as expected.
/// </summary>
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.ObjectModel")]
public class NestingOptimizationRequestUsingObjectModelTests : IntelliDivideTestBase
{
    /// <summary />
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, CommonSampleSettings.TimeoutDuration);
    }

    /// <summary />
    [TestMethod]
    public async Task NestingRequest_ObjectModel_RequiredProperties_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task NestingRequest_ObjectModel_MprProgramVariables_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_MprProgramVariables_ImportOnly(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task NestingRequest_ObjectModel_RequiredProperties_Optimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_Optimize(intelliDivide);
    }
}