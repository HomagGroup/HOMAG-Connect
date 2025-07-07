using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Result;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Optimizations
{
    /// <summary />
    public static class OptimizationRetrievalSamples
    {
        /// <summary>
        /// The example shows how to get the first three optimizations.
        /// </summary>
        public static async Task Optimizations_GetFirstFiveOptimizations(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizations(3);

            optimization.Trace();
        }

        /// <summary>
        /// The example shows how to get the first three optimized cutting optimizations.
        /// </summary>
        public static async Task Optimizations_GetOptimizationsWithStatusOptimized(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 3).ToListAsync();
            
            optimizations.Trace();
        }

        /// <summary>
        /// The example shows how to get the first three cutting optimizations.
        /// </summary>
        public static async Task Optimizations_GetOptimizationsOfTypeCutting(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizations(OptimizationType.Cutting, 3).ToListAsync();

            optimizations.Trace();
        }

        /// <summary>
        /// The example shows how to get the <see cref="OptimizationStatus" /> of an optimization.
        /// </summary>
        public static async Task Optimizations_GetStatusOfAOptimization(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 1).FirstAsync();

            var optimizationStatus = await intelliDivide.GetOptimizationStatus(optimization.Id);

            optimizationStatus.Trace();
        }

        /// <summary>
        /// The example shows how to get the list of all available solutions and the details of the balanced solution.
        /// </summary>
        public static async Task Optimizations_GetSolutionsAndSolutionDetails(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 1).FirstOrDefaultAsync();

            if (optimization == null)
            {
                Assert.Inconclusive("No optimized having the state optimization found.");
            }

            // Get the solutions including the main key figures
            var solutions = await intelliDivide.GetSolutions(optimization.Id).ToListAsync();
            solutions.Trace(nameof(solutions));

            // Get the details of the first solution which is the balanced solution
            var balancedSolution = await intelliDivide.GetSolutionDetails(optimization.Id, solutions.First().Id);
            balancedSolution.Trace(nameof(balancedSolution));

            // Download the saw file
            var sawFile = new FileInfo(optimization.Name + ".saw");
            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolution.Id, SolutionExportType.Saw, sawFile);
            sawFile.FullName.Trace();

            // Download the pdf file
            var pdfFile = new FileInfo(optimization.Name + ".pdf");
            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolution.Id, SolutionExportType.Pdf, pdfFile);
            pdfFile.FullName.Trace();

            // Download the material demand file
            var excelFile = new FileInfo(optimization.Name + ".xlsx");
            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolution.Id, SolutionExportType.MaterialDemand, excelFile);
            excelFile.FullName.Trace();
        }

        /// <summary>
        /// The example shows how to archive and delete an optimization.
        /// </summary>
        public static async Task Optimizations_ArchiveAndDelete(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 1).FirstOrDefaultAsync();

            if (optimization == null)
            {
                Assert.Inconclusive("No optimized having the state optimization found.");
            }

            await intelliDivide.ArchiveOptimization(optimization.Id);

            await intelliDivide.DeleteOptimization(optimization.Id);
        }

        /// <summary>
        /// The example shows how to get a list of the available exports for a solution of an optimization.
        /// </summary>
        public static async Task Optimizations_GetExportsForSolution(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizations(OptimizationType.Cutting, OptimizationStatus.Optimized, 1).FirstOrDefaultAsync();

            if (optimization == null)
            {
                Assert.Inconclusive("No optimized having the state optimization found.");
            }

            var solutions = await intelliDivide.GetSolutions(optimization.Id).ToListAsync();

            var exports = await intelliDivide.GetSolutionAvailableExports(optimization.Id, solutions.First().Id);
            
            exports.Trace();
        }
    }
}