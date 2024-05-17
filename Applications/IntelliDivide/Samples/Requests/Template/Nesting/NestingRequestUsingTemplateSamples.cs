using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Samples.Helper;

namespace HomagConnect.IntelliDivide.Samples.Requests.Template.Nesting
{
    /// <summary>
    /// Nesting request samples using a structured file (Excel, CSV, PNX, ...), the referenced MPRs and a template.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Template/Nesting/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class NestingRequestUsingTemplateSamples
    {
        /// <summary>
        /// Gets the materials used in the samples.
        /// </summary>
        public static string[] SampleMaterialCodes = { "P2_White_19", "P2_Gold Craft Oak_19" };

        /// <summary>
        /// The sample shows how to create a nesting request using a structured file (Excel, CSV, PNX, ...), the referenced MPRs
        /// and a template.
        /// </summary>
        public static async Task NestingRequest_Template_CSV_MPR_ImportAndOptimize(IIntelliDivideClient intelliDivide)
        {
            var request = new OptimizationRequestUsingTemplate
            {
                Name = "HOMAG Connect - Template_CSV_MPR_ImportAndOptimize",
                Machine = "productionAssist Nesting",
                Parameters = "Default Nesting",
                ImportTemplate = "CSV-MPR template",
                Action = OptimizationRequestAction.Optimize
            };

            var importFile = await ImportFile.CreateAsync(@"Requests\Template\Nesting\Kitchen.zip");

            var response = await intelliDivide.RequestOptimizationAsync(request, importFile);

            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromMinutes(2));

            optimization.Trace();

            var recommendedSolution = await intelliDivide.GetSolutionsAsync(optimization.Id).FirstAsync();

            await intelliDivide.DownloadSolutionExportAsync(recommendedSolution, SolutionExportType.ZIP, new DirectoryInfo("."));
        }

        /// <summary>
        /// The sample shows how to create a nesting request using a structured file (Excel, CSV, PNX, ...), the referenced MPRs
        /// and a template.
        /// </summary>
        public static async Task NestingRequest_Template_CSV_MPR_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var request = new OptimizationRequestUsingTemplate
            {
                Name = "HOMAG Connect - Template_CSV_MPR_ImportOnly",
                Machine = "productionAssist Nesting",
                Parameters = "Default Nesting",
                ImportTemplate = "CSV-MPR template"
            };

            var importFile = await ImportFile.CreateAsync(@"Requests\Template\Nesting\Kitchen.zip");

            var response = await intelliDivide.RequestOptimizationAsync(request, importFile);

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace();

        }
    }
}