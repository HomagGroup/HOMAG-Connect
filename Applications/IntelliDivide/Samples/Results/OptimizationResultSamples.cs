using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Constants;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Results
{
    /// <summary />
    public static class OptimizationResultSamples
    {
        private const int _Take = 3;

        /// <summary />
        public static async Task GetOptimizationSample(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizationsAsync(1);

            optimization.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationsHavingStatusOptimized(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take).ToListAsync();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.All(o => o.Status == OptimizationStatus.Optimized));

            optimizations.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationsOfTypeCuttingSample(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, _Take).ToListAsync();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.All(o => o.OptimizationType == OptimizationType.Cutting));

            optimizations.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationsSample(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizationsAsync(_Take).ToListAsync();

            Assert.IsNotNull(optimizations);
            Assert.IsTrue(optimizations.Any());
            Assert.IsTrue(optimizations.Count <= _Take);

            optimizations.Trace();
        }

        /// <summary />
        public static async Task GetOptimizationStatusSample(IIntelliDivideClient intelliDivide)
        {
            var optimization = await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take).FirstAsync();

            Assert.AreEqual(OptimizationStatus.Optimized, optimization.Status);

            var optimizationStatus = await intelliDivide.GetOptimizationStatusAsync(optimization.Id);

            Assert.AreEqual(OptimizationStatus.Optimized, optimizationStatus);

            optimizationStatus.Trace();
        }

        /// <summary />
        public static async Task GetSolutionDetailsSample(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take).ToListAsync();

            if (optimizations == null || !optimizations.Any())
            {
                Assert.Inconclusive("No optimizations having the status Optimized have been found.");
            }

            var optimization = optimizations.First(o => o.Status == OptimizationStatus.Optimized);
            var optimizationSolutions = await intelliDivide.GetSolutionsAsync(optimization.Id).ToListAsync();

            var balancedSolution = optimizationSolutions.Where(s => s.Name == SolutionName.BalancedSolution);

            Assert.IsNotNull(balancedSolution);

            balancedSolution.Trace();
        }

        /// <summary />
        public static async Task GetSolutionsSample(IIntelliDivideClient intelliDivide)
        {
            var optimizations = await intelliDivide.GetOptimizationsAsync(OptimizationType.Cutting, OptimizationStatus.Optimized, _Take).ToListAsync();
            
            if (optimizations == null || !optimizations.Any())
            {
                 Assert.Inconclusive("No optimizations having the status Optimized have been found.");
            }

            var optimization = optimizations.First(o => o.Status == OptimizationStatus.Optimized);
            var optimizationSolutions = await intelliDivide.GetSolutionsAsync(optimization.Id).ToListAsync();

            Assert.IsNotNull(optimizationSolutions);

            optimizationSolutions.Trace();
        }
    }
}