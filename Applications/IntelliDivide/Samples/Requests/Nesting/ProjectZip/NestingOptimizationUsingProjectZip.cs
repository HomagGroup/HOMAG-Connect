﻿using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

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
        private const string _ProjectFile = @"Requests\Nesting\ProjectZip\nesting_project_with_mpr_files.zip";

        /// <summary>
        /// The sample shows how to import a structured ZIP file for nesting optimization.
        /// </summary>
        public static async Task NestingRequest_ProjectZip_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(_ProjectFile);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync(m => m.Name == "productionAssist Nesting");
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

        /// <summary>
        /// The example shows how to import a structured ZIP file for nesting optimization and start the optimization.
        /// </summary>
        public static async Task NestingRequest_ProjectZip_Optimize(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(_ProjectFile);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync(m => m.Name == "productionAssist Nesting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.Optimize
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, projectFile);

            response.Trace();

            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            optimization.Trace();
        }
    }
}