using System.Globalization;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;

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

            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            optimization.Trace();

            var recommendedSolution = await intelliDivide.GetSolutions(optimization.Id).FirstAsync();

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

            response.Trace();

            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);

            optimization.Trace();
        }
    }
}