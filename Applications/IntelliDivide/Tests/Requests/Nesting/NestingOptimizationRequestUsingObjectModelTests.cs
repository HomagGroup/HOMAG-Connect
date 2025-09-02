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
    /// <Remarks>
    /// System.TimeoutException: The operation has timed out.
    /// Stack Trace:
    /// at HomagConnect.IntelliDivide.Client.IntelliDivideClient.WaitForOptimizationStatus(Guid optimizationId, OptimizationStatus optimizationStatus, TimeSpan maxDuration) in C:\a\1\s\Applications\IntelliDivide\Client\IntelliDivideClient.cs:line 444
    /// at HomagConnect.IntelliDivide.Client.IntelliDivideClient.WaitForCompletion(Guid optimizationId, TimeSpan maxDuration) in C:\a\1\s\Applications\IntelliDivide\Client\IntelliDivideClient.cs:line 393
    /// at HomagConnect.IntelliDivide.Samples.Requests.Nesting.ObjectModel.NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_Optimize(IIntelliDivideClient intelliDivide) in C:\a\1\s\Applications\IntelliDivide\Samples\CSharp\Requests\Nesting\ObjectModel\NestingRequestUsingObjectModelSamples.cs:line 169
    /// at HomagConnect.IntelliDivide.Tests.Requests.Nesting.NestingOptimizationRequestUsingObjectModelTests.NestingRequest_ObjectModel_RequiredProperties_Optimize() in C:\a\1\s\Applications\IntelliDivide\Tests\Requests\Nesting\NestingOptimizationRequestUsingObjectModelTests.cs:line 51
    /// 
    /// Standard Output Messages:
    /// request
    /// {"name":"Nesting Sample-Optimize-20250902-0714","parts":[{"unitSystem":"Metric","description":"PartA.mpr","materialCode":"P2_Gold_Craft_Oak_19.0","grain":"Lengthwise","quantity":2,"quantityPlus":0,"mprFileName":"PartA.mpr","mprProgramVariables":[{"name":"L","value":"980"},{"name":"B","value":"450"}]},{ "unitSystem":"Metric","description":"PartB.mpr","materialCode":"P2_White_19.0","grain":"None","quantity":5,"quantityPlus":0,"mprFileName":"PartB.mpr"}],"action":"Optimize","boards":[],"machine":"CENTATEQ N/BHP #0","parameters":"Default Nesting"}
    /// 
    /// response
    /// {"optimizationId":"f9001094-a6d6-4546-8eda-d9c6f760be82","optimizationStatus":"Started","link":"https://intellidivide.homag.cloud/#/84959295-afb1-4177-b087-840a660a90e4/jobs/f9001094-a6d6-4546-8eda-d9c6f760be82"}
    /// 
    /// </Remarks>
    [TestMethod]
    [Ignore] // Ignored due to occasional timeouts in CI/CD pipeline
    public async Task NestingRequest_ObjectModel_RequiredProperties_Optimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_Optimize(intelliDivide);
    }
}