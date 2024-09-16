using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.Common")]
public class NestingOptimizationRequestCommonTests : IntelliDivideTestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, CommonSampleSettings.TimeoutDuration);
    }





    [TestMethod]
    public async Task CreateNestingOptimizationRequestByObjectModelOptimizeAndRetrieveResults()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelOptimizeAndRetrieveResults(intelliDivide);
    }

}