using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Constants;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Results
{
    /// <summary />
    public class OptimizationResultSamples
    {
        private const int _Take = 3;

        /// <summary />
        public static async Task GetOptimizationSample(IIntelliDivideClient intelliDivide)
        {
            var optimization = (await intelliDivide.GetOptimizationsAsync(1)).First();
            var optimizationDetails = (await intelliDivide.GetOptimizationAsync(optimization.Id));

            Assert.IsNotNull(optimization);
            Assert.AreEqual(optimization.Status, optimizationDetails.Status);

            optimization.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationsHavingStatusOptimized(IIntelliDivideClient intelliDivide)
        {
            var optimizations = (await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take)).ToArray();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.All(o => o.Status == OptimizationStatus.Optimized));

            optimizations.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationsOfTypeCuttingSample(IIntelliDivideClient intelliDivide)
        {
            var optimizations = (await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, _Take)).ToArray();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.All(o => o.OptimizationType == OptimizationType.Cutting));

            optimizations.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationsSample(IIntelliDivideClient intelliDivide)
        {
            var optimizations = (await intelliDivide.GetOptimizationsAsync(_Take)).ToArray();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.Any());
            Assert.IsTrue(optimizations.Length <= _Take);

            optimizations.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationStatusSample(IIntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(_Take)).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimizationStatus = await intelliDivide.GetOptimizationStatusAsync(optimizationId);

            Assert.AreEqual(OptimizationStatus.Optimized, optimizationStatus);

            optimizationStatus.Trace();
        }

        /// <summary />
        public static async Task GetSolutionDetailsSample(IIntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take)).First().Id;

            var optimizationSolutions = await intelliDivide.GetSolutionsAsync(optimizationId);

            var balancedSolution = optimizationSolutions.Where(s => s.Name == SolutionName.BalancedSolution);

            Assert.IsNotNull(balancedSolution);

            balancedSolution.Trace();
        }

        /// <summary />
        public static async Task GetSolutionsSample(IIntelliDivideClient intelliDivide)
        {
            var optimizationId = (await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take)).First(o => o.Status == OptimizationStatus.Optimized).Id;

            var optimizationSolutions = await intelliDivide.GetSolutionsAsync(optimizationId);

            Assert.IsNotNull(optimizationSolutions);

            optimizationSolutions.Trace();
        }
    }
}