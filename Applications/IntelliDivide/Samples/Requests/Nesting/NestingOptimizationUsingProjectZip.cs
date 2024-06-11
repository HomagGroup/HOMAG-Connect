using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Nesting
{
    /// <summary />
    public static class NestingOptimizationUsingProjectZip
    {
        /// <summary />
        public static async Task CreateNestingOptimizationUsingProjectZip(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Requests\Nesting\nesting_project_with_mpr_files.zip");

            Assert.IsTrue(projectFile.Exists);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync(m => m.Name == "productionAssist Nesting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, projectFile);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.New, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationResults.Any());

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.New, optimization.Status);
        }

        /// <summary />
        public static async Task CreateNestingOptimizationUsingProjectZipAndOptimize(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Requests\Nesting\nesting_project_with_mpr_files.zip");

            Assert.IsTrue(projectFile.Exists);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync(m => m.Name == "productionAssist Nesting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.Optimize
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, projectFile);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.Started, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationResults.Any());

            response.Trace();

            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromMinutes(2));

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.Optimized, optimization.Status);

            optimization.Trace();
        }

        /// <summary />
        public static async Task CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Requests\Nesting\nesting_project_with_mpr_files.zip");

            Assert.IsTrue(projectFile.Exists);

            var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync(m => m.Name == "productionAssist Nesting");
            var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();

            var request = new OptimizationRequestUsingProject
            {
                Machine = optimizationMachine.Name,
                Parameters = optimizationParameter.Name,
                Action = OptimizationRequestAction.Send
            };

            var response = await intelliDivide.RequestOptimizationAsync(request, projectFile);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.Started, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationResults.Any());

            response.Trace();

            var optimization = await intelliDivide.WaitForOptimizationStatusAsync(response.OptimizationId, OptimizationStatus.Transferred, TimeSpan.FromMinutes(1));

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.Transferred, optimization.Status);

            optimization.Trace();
        }
    }
}
