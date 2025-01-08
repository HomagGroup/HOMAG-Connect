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
        public static async Task CuttingRequest_ProjectZip_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Data\Cutting\Project.zip");

            Assert.IsTrue(projectFile.Exists);

            var optimizationMachine = await intelliDivide.GetMachines(OptimizationType.Cutting).FirstAsync(m => m.Name == "productionAssist Cutting");
            var optimizationParameter = await intelliDivide.GetParameters(optimizationMachine.OptimizationType).FirstAsync();

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