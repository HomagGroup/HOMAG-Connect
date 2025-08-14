using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting.Project
{
    /// <summary>
    /// Cutting request samples using a structured ZIP file in a specified format.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Cutting/Project/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class CuttingOptimizationUsingProjectZip
    {
        /// <summary />
        public static async Task CuttingRequest_ProjectZip_ImportOnly(IIntelliDivideClient intelliDivide, string? zipFileName = null, string? optimizationParametersName = null)
        {
            var fileName = string.IsNullOrEmpty(zipFileName) ? "Project.zip" : zipFileName;
            var projectFile = new FileInfo(@$"Data\Cutting\{fileName}");

            Assert.IsTrue(projectFile.Exists);

            var optimizationMachine = await intelliDivide.GetMachines(OptimizationType.Cutting).FirstOrDefaultAsync(m => m.Name == "productionAssist Cutting");
            if (optimizationMachine == null)
            {
                Assert.Inconclusive("The machine is not available.");
            }

            var optimizationParameter = await intelliDivide.GetParameters(optimizationMachine.OptimizationType)
                .FirstOrDefaultAsync(p => string.IsNullOrEmpty(optimizationParametersName) || p.Name.Equals(optimizationParametersName, StringComparison.Ordinal));
            if (optimizationParameter == null)
            {
                Assert.Inconclusive("The optimizing parameter is not available.");
            }

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            var response = await intelliDivide.RequestOptimization(request, projectFile);

            response.Trace();

            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);

            optimization.Trace();
        }
    }
}