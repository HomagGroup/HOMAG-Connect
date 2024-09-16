using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting.Template;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Nesting")]
[TestCategory("IntelliDivide.Requests.Nesting.Template")]
public class NestingOptimizationRequestUsingTemplateTests : IntelliDivideTestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialCodesExists(CommonSampleSettings.SampleMaterialCodeGrainLengthwise, CommonSampleSettings.SampleMaterialCodeGrainNone);
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, CommonSampleSettings.TimeoutDuration);
    }

    private static async Task EnsureImportTemplateExists(IIntelliDivideClient intelliDivide, OptimizationType optimizationType, string importTemplateName)
    {
        var optimizationImportTemplates = await intelliDivide.GetImportTemplatesAsync(optimizationType).ToListAsync();

        if (optimizationImportTemplates.All(t => t.Name != importTemplateName))
        {
            Assert.Inconclusive($"The import template '{importTemplateName}' does not exist.");
        }
    }




    [TestMethod]
    public async Task NestingRequest_Template_CSV_MPR_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportOnly(intelliDivide);
    }


    [TestMethod]
    public async Task NestingRequest_Template_CSV_MPR_ImportAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await EnsureImportTemplateExists(intelliDivide, OptimizationType.Nesting, "CSV-MPR template");

        await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportAndOptimize(intelliDivide);
    }

}