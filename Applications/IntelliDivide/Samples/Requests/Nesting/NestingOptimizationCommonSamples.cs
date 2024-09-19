using System.Globalization;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Constants;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting.ObjectModel;

namespace HomagConnect.IntelliDivide.Samples.Requests.Nesting
{
    /// <summary>
    /// The class contains examples of nesting optimization requests that are common to requests created from the object model,
    /// an import template, or the project zip file.
    /// </summary>
    public static class NestingOptimizationCommonSamples
    {
        /// <summary>
        /// The example shows how to retrieve the solution details and download the optimized solution, documentation, and material requirements.
        /// </summary>
        public static async Task NestingRequest_Common_RetrieveResults(IIntelliDivideClient intelliDivide)
        {
            var mprFiles = new List<ImportFile>();

            var machine = await intelliDivide.GetMachines(OptimizationType.Nesting).FirstAsync(m => m.Name.Contains("CENTATEQ"));
            var parameter = await intelliDivide.GetParameters(machine.OptimizationType).FirstAsync();

            var request = await NestingRequestUsingObjectModelSamples.GetSampleNestingOptimizationByObjectModel(mprFiles);

            request.Name = "Sample_Common_RetrieveResults" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;
            request.Action = OptimizationRequestAction.Optimize;

            request.Trace(nameof(request));

            var response = await intelliDivide.RequestOptimization(request, mprFiles.ToArray());

            response.Trace(nameof(response));

            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            if (optimization.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException($"Optimization did not reach the state optimized. (Status: {optimization.Status}");
            }

            optimization.Trace(nameof(optimization));

            var solutions = await intelliDivide.GetSolution(optimization.Id).ToListAsync() ?? throw new InvalidOperationException("Solutions could not get retrieved.");
            solutions.Trace(nameof(solutions));

            var balancedSolutionDetails = await intelliDivide.GetSolutionDetails(optimization.Id, solutions.First(s => s.Name == SolutionName.BalancedSolution).Id);

            balancedSolutionDetails.Trace(nameof(balancedSolutionDetails));

            // Download the zip file which contains the optimized solution
            var zipFileName = new FileInfo($"{request.Name}.zip");
            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolutionDetails.Id, SolutionExportType.ZIP, zipFileName);
            zipFileName.FullName.Trace();

            // Download the zip file which contains the documentation
            var pdfFileName = new FileInfo($"{request.Name}.pdf");
            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolutionDetails.Id, SolutionExportType.Pdf, pdfFileName);
            pdfFileName.FullName.Trace();

            // Download the zip file which contains the material demand
            var excelFileName = new FileInfo($"{request.Name}.xlsx");
            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolutionDetails.Id, SolutionExportType.MaterialDemand, excelFileName);
            excelFileName.FullName.Trace();
        }
    }
}