using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting.ObjectModel;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.ObjectModel")]
public class NestingOptimizationRequestUsingObjectModelTests : IntelliDivideTestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, CommonSampleSettings.TimeoutDuration);
    }


    [TestMethod]
    public async Task CreateNestingOptimizationRequestByObjectModelAndOptimizeAndSend()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelAndOptimizeAndSend(intelliDivide);
    }

    [TestMethod]
    public async Task NestingRequest_ObjectModel_RequiredProperties_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_ImportOnly(intelliDivide);
    }


    [TestMethod]
    public async Task NestingRequest_ObjectModel_MprProgramVariables_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_MprProgramVariables_ImportOnly(intelliDivide);
    }

    [TestMethod]
    public async Task CreateNestingOptimizationRequestByObjectModel()

    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModel(intelliDivide);
    }

    [TestMethod]
    public async Task CreateNestingOptimizationRequestByObjectModelAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelAndOptimize(intelliDivide);
    }


}