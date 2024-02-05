using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Constants;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Results
{
    public class OptimizationResultSamples
    {
        private const int _Take = 3;

        public static async Task GetOptimizationSample(IntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(_Take)).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimization = (await intelliDivide.GetOptimizationAsync(optimizationId));

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.Optimized, optimization.Status);

            optimization.Trace();
        }

        public static async Task GetOptimizationsOfTypeCuttingSample(IntelliDivideClient intelliDivide)
        {
            var optimizations = (await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, _Take)).ToArray();
            
            Assert.IsNotNull(optimizations);

            optimizations.Trace();
        }

        public static async Task GetOptimizationsHavingStatusOptimized(IntelliDivideClient intelliDivide)
        {
            var optimizations = (await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take)).ToArray();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.All(o => o.Status == OptimizationStatus.Optimized));

            optimizations.Trace();
        }

        public static async Task GetOptimizationsSample(IntelliDivideClient intelliDivide)
        {
            var optimizations = (await intelliDivide.GetOptimizationsAsync(_Take)).ToArray();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.Any());
            Assert.IsTrue(optimizations.Length <= _Take);

            optimizations.Trace();
        }

        public static async Task GetOptimizationStatusSample(IntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(_Take)).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimizationStatus = await intelliDivide.GetOptimizationStatusAsync(optimizationId);

            Assert.AreEqual(OptimizationStatus.Optimized, optimizationStatus);

            optimizationStatus.Trace();
        }

        public static async Task GetSolutionDetailsSample(IntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(_Take)).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimizationSolutions = await intelliDivide.GetSolutionsAsync(optimizationId);

            var balancedSolution = optimizationSolutions.Where(s => s.Name == SolutionName.BalancedSolution);

            Assert.IsNotNull(balancedSolution);

            balancedSolution.Trace();
        }

        public static async Task GetSolutionsSample(IntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(_Take)).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimizationSolutions = await intelliDivide.GetSolutionsAsync(optimizationId);

            Assert.IsNotNull(optimizationSolutions);

            optimizationSolutions.Trace();
        }
    }
}