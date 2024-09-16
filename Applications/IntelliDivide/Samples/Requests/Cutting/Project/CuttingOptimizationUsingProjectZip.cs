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
        private const string _ProjectZipSampleFile = @"Requests\Cutting\Project\Project.zip";

        /// <summary />
        public static async Task CreatedCuttingOptimizationUsingProjectZip(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(_ProjectZipSampleFile);

            Assert.IsTrue(projectFile.Exists);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Cutting).FirstAsync(m => m.Name == "productionAssist Cutting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, projectFile);

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace();
        }
    }
}