using HomagConnect.Base.Extensions;
using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting;
using HomagConnect.IntelliDivide.Samples.Requests.ObjectModel.Nesting;
using HomagConnect.IntelliDivide.Samples.Requests.Template.Nesting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Requests.Nesting")]
    public class NestingOptimizationRequestTests : IntelliDivideTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestInitialize]
        public async Task Initialize()
        {
            await EnsureSampleMaterialCodesExists(NestingRequestUsingObjectModelSamples.SampleMaterialCodeGrainLengthwise, NestingRequestUsingObjectModelSamples.SampleMaterialCodeGrainNone);
            await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, TimeSpan.FromMinutes(2));
        }
        private async Task EnsureImportTemplateExists(IIntelliDivideClient intelliDivide,OptimizationType optimizationType, string importTemplateName)
        {
            var optimizationImportTemplates = await intelliDivide.GetImportTemplatesAsync(optimizationType).ToListAsync();  

            if (optimizationImportTemplates.All(t => t.Name != importTemplateName))
            {
               Assert.Inconclusive($"The import template '{importTemplateName}' does not exist.");
            }
        }


        [TestMethod]
        public async Task NestingRequest_ObjectModel_RequiredProperties_ImportOnly()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_ImportOnly(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 7, 15)] // todo: reenable tests divide!
        public async Task NestingRequest_Template_CSV_MPR_ImportOnly()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportOnly(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 9, 1)]

        public async Task NestingRequest_Template_CSV_MPR_ImportAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await EnsureImportTemplateExists(intelliDivide, OptimizationType.Nesting, "CSV-MPR template");

            await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportAndOptimize(intelliDivide);
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

        [TestMethod]
        [TemporaryDisabledOnServer(2024,9,1)]
        public async Task CreateNestingOptimizationRequestByObjectModelAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelAndOptimizeAndSend(intelliDivide);
        }

        [TestMethod]
        public async Task CreateNestingOptimizationUsingProjectZip()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZip(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 9, 1)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimize(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 9, 1)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 9, 1)]
        public async Task CreateNestingOptimizationRequestByObjectModelOptimizeAndRetrieveResults()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelOptimizeAndRetrieveResults(intelliDivide);
        }
    }
#pragma warning restore S2699 // Tests should include assertions
}