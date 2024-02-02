using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting
{
    public class CuttingOptimizationUsingProjectZip
    {
        public static async Task CreatedCuttingOptimizationUsingProjectZip(IntelliDivideClient intelliDivide)
        {
            var projectFile = new FileInfo(@"Requests\Cutting\Project.zip");

            Assert.IsTrue(projectFile.Exists);

            var response = await intelliDivide.RequestOptimizationAsync(projectFile);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.New, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationErrors.Any());

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.New, optimization.Status);
        }
    }
}