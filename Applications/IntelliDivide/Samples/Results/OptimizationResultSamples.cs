using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Results
{
    public class OptimizationResultSamples
    {
        public static async Task GetOptimizationSample(IntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync()).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimization = (await intelliDivide.GetOptimizationAsync(optimizationId));

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.Optimized, optimization.Status);

            optimization.Trace();
        }

        public static async Task GetOptimizationStatusSample(IntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync()).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimizationStatus = (await intelliDivide.GetOptimizationStatusAsync(optimizationId));
            
            Assert.AreEqual(OptimizationStatus.Optimized, optimizationStatus);

            optimizationStatus.Trace();
        }

        public static async Task GetOptimizationSolutionsSample(IntelliDivideClient intelliDivide)
        {
            var optimization = (await intelliDivide.GetOptimizationsAsync()).FirstOrDefault(o => o.Status == OptimizationStatus.Optimized);
        }

        public static async Task GetOptimizationsSample(IntelliDivideClient intelliDivide)
        {
            const uint take = 3;

            var optimizations = (await intelliDivide.GetOptimizationsAsync(0, take)).ToArray();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.Any());
            Assert.IsTrue(optimizations.Length <= take);

            optimizations.Trace();
        }
    }
}