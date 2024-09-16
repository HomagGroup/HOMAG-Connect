using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting.Template
{
    /// <summary>
    /// Cutting request samples using a structured file (Excel, CSV, PNX, ...) and a template.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Cutting/Template/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class CuttingRequestUsingTemplateSamples
    {
        private const string _ExcelSampleFile = @"Requests\Cutting\Template\Kitchen.xlsx";

        /// <summary>
        /// The sample shows how to create a cutting request using a structured file (Excel, CSV, PNX, ...) and a template.
        /// template.
        /// </summary>
        public static async Task CuttingRequest_Template_Excel_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var importFile = await ImportFile.CreateAsync(_ExcelSampleFile);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Cutting).FirstAsync(m => m.Name == "productionAssist Cutting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();
            var importTemplate = await intelliDivide.GetImportTemplatesAsync(optimizationMachine.OptimizationType, importFile.Extension)
                .FirstAsync(i => i.Name.IndexOf("homag.cloud", StringComparison.InvariantCultureIgnoreCase) >= 0);

            var request = new OptimizationRequestUsingTemplate
            {
                Name = "HOMAG Connect - Template_Excel_ImportOnly",
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                ImportTemplate = importTemplate.Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, importFile);

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace();
        }

        /// <summary>
        /// The sample shows how to create a cutting request using a structured file (Excel, CSV, PNX, ...) and a template.
        /// template.
        /// </summary>
        public static async Task CuttingRequest_Template_Excel_ImportAndOptimize(IIntelliDivideClient intelliDivide)
        {
            var importFile = await ImportFile.CreateAsync(_ExcelSampleFile);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Cutting).FirstAsync(m => m.Name == "productionAssist Cutting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();
            var importTemplate = await intelliDivide.GetImportTemplatesAsync(optimizationMachine.OptimizationType, importFile.Extension)
                .FirstAsync(i => i.Name.IndexOf("homag.cloud", StringComparison.InvariantCultureIgnoreCase) >= 0);

            var request = new OptimizationRequestUsingTemplate
            {
                Name = "HOMAG Connect - Template_Excel_ImportAndOptimize",
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                ImportTemplate = importTemplate.Name,
                Action = OptimizationRequestAction.Optimize
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, importFile);

            response.Trace();

            if (!response.ValidationResults.Any() && response.OptimizationStatus is OptimizationStatus.New or OptimizationStatus.Started or OptimizationStatus.Optimized)
            {
                var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

                optimization.Trace();

                var recommendedSolution = await intelliDivide.GetSolutionsAsync(optimization.Id).FirstAsync();

                await intelliDivide.DownloadSolutionExportAsync(recommendedSolution, SolutionExportType.Saw, new DirectoryInfo("."));
            }
            else
            {
                throw new InternalTestFailureException();
            }
        }
    }
}