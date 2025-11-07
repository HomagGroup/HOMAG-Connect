using System.Globalization;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Nesting.Template
{
    /// <summary>
    /// Nesting request samples using a structured file (Excel, CSV, PNX, ...), the referenced MPRs and a template.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Nesting/Template/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class NestingRequestUsingTemplateSamples
    {
        /// <summary>
        /// The sample shows how to create a nesting request using a structured file (Excel, CSV, PNX, ...), the referenced MPRs
        /// and a template.
        /// </summary>
        public static async Task NestingRequest_Template_CSV_MPR_ImportAndOptimize(IIntelliDivideClient intelliDivide)
        {
            var request = new OptimizationRequestUsingTemplate
            {
                Name = "Sample_Template_CSV_MPR_ImportAndOptimize" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Nesting",
                Parameters = (await intelliDivide.GetParameters(OptimizationType.Nesting).FirstAsync()).Name,
                ImportTemplate = CommonSampleSettings.NestingImportTemplateName,
                Action = OptimizationRequestAction.Optimize
            };

            var importFile = await ImportFile.CreateAsync(@"Data\Nesting\Kitchen.zip");

            var response = await intelliDivide.RequestOptimization(request, importFile);
            if (response == null)
            {
                Assert.Inconclusive("The request did not send a response.");
            }

            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);
            if (optimization == null)
            {
                Assert.Inconclusive($"The optimization with id {response.OptimizationId} wasn't completed.");
            }

            optimization.Trace();

            var recommendedSolution = await intelliDivide.GetSolutions(optimization.Id).FirstAsync();
            if (recommendedSolution == null)
            {
                Assert.Inconclusive($"The solutions for the optimization with id {optimization.Id} should have at least one element.");
            }

            await intelliDivide.DownloadSolutionExport(recommendedSolution, SolutionExportType.ZIP, new DirectoryInfo("."));
        }

        /// <summary>
        /// The sample shows how to create a nesting request using a structured file (Excel, CSV, PNX, ...), the referenced MPRs
        /// and a template.
        /// </summary>
        public static async Task NestingRequest_Template_CSV_MPR_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var request = new OptimizationRequestUsingTemplate
            {
                Name = "Sample_Template_CSV_MPR_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Nesting",
                Parameters = "Default Nesting",
                ImportTemplate = CommonSampleSettings.NestingImportTemplateName
            };

            var importFile = await ImportFile.CreateAsync(@"Data\Nesting\Kitchen.zip");

            var response = await intelliDivide.RequestOptimization(request, importFile);
            if (response == null)
            {
                Assert.Inconclusive("The request did not send a response.");
            }

            response.Trace();

            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);
            if (optimization == null)
            {
                Assert.Inconclusive($"The optimization with id {response.OptimizationId} could not be found.");
            }

            optimization.Trace();
        }
    }
}