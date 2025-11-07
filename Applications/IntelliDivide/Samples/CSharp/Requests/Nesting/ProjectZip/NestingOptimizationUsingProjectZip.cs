using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Nesting.ProjectZip
{
    /// <summary>
    /// Nesting request samples using a structured ZIP file in a specified format.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Nesting/Project/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class NestingOptimizationUsingProjectZip
    {
        /// <summary>
        /// The sample shows how to import a structured ZIP file for nesting optimization.
        /// </summary>
        public static async Task NestingRequest_ProjectZip_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Data\Nesting\Project.zip");

            var optimizationMachine = await intelliDivide.GetMachines(OptimizationType.Nesting).FirstAsync(m => m.Name == "productionAssist Nesting");
            var optimizationParameter = await intelliDivide.GetParameters(optimizationMachine.OptimizationType).FirstAsync();

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            var response = await intelliDivide.RequestOptimization(request, projectFile);
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

        /// <summary>
        /// The example shows how to import a structured ZIP file for nesting optimization and start the optimization.
        /// </summary>
        public static async Task NestingRequest_ProjectZip_Optimize(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Data\Nesting\Project.zip");

            var optimizationMachine = await intelliDivide.GetMachines(OptimizationType.Nesting).FirstOrDefaultAsync(m => m.Name == "productionAssist Nesting");
            if (optimizationMachine == null)
            {
                Assert.Inconclusive("The machine is not available.");
            }
            var optimizationParameter = await intelliDivide.GetParameters(optimizationMachine.OptimizationType).FirstOrDefaultAsync();
            if (optimizationParameter == null)
            {
                Assert.Inconclusive("There is no optimizing parameter available.");
            }

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.Optimize
            };

            var response = await intelliDivide.RequestOptimization(request, projectFile);
            if (response == null)
            {
                Assert.Inconclusive("The request did not send a response.");
            }

            response.Trace();

            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);
            if (optimization == null)
            {
                Assert.Inconclusive($"The optimization with id {response.OptimizationId} wasn't completed.");
            }

            optimization.Trace();
        }
    }
}