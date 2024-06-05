using HomagConnect.Base.Tests.Attributes;
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
            await EnsureSampleMaterialExists(NestingRequestUsingObjectModelSamples.SampleMaterialCodes.Union(NestingRequestUsingTemplateSamples.SampleMaterialCodes));

            await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Nesting, TimeSpan.FromMinutes(2));
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 15)] // todo: reenable tests divide!
        public async Task NestingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_RequiredProperties_ImportOnly(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 15)] // todo: reenable tests divide!
        public async Task NestingRequest_Template_CSV_MPR_ImportOnly()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportOnly(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 1)] // todo: reenable tests divide!
        public async Task NestingRequest_Template_CSV_MPR_ImportAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingRequestUsingTemplateSamples.NestingRequest_Template_CSV_MPR_ImportAndOptimize(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 1)] // todo: reenable tests divide!
        public async Task NestingRequest_ObjectModel_MprProgramVariables_ImportOnly()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingRequestUsingObjectModelSamples.NestingRequest_ObjectModel_MprProgramVariables_ImportOnly(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 1)] // todo: reenable tests divide!
        public async Task CreateNestingOptimizationRequestByObjectModel()

        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModel(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 1)] // todo: reenable tests divide!
        public async Task CreateNestingOptimizationRequestByObjectModelAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelAndOptimize(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 1)] // todo: reenable tests divide!
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
        [TemporaryDisabledOnServer(2024, 6, 15)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimize(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 15)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 15)]
        public async Task CreateNestingOptimizationRequestByObjectModelOptimizeAndRetrieveResults()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelOptimizeAndRetrieveResults(intelliDivide);
        }
    }
#pragma warning restore S2699 // Tests should include assertions
}