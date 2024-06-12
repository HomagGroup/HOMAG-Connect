using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting
{
    /// <summary />
    public static class CuttingOptimizationUsingProjectZip
    {
        /// <summary />
        public static async Task CreatedCuttingOptimizationUsingProjectZip(IIntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Requests\Cutting\Project.zip");

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

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.New, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationResults.Any());

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.New, optimization.Status);
        }
    }
}