using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class ParameterSamples
    {
        /// <summary />
        public static async Task GetCuttingParametersSample(IIntelliDivideClient intelliDivide)
        {
            var parameters = await intelliDivide.GetParameters(OptimizationType.Cutting).ToListAsync();

            Assert.IsNotNull(parameters);
            Assert.IsTrue(parameters.Any());
            Assert.IsFalse(parameters.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(parameters.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            parameters.Trace();
        }

        /// <summary />
        public static async Task GetNestingParametersSample(IIntelliDivideClient intelliDivide)
        {
            var parameters = await intelliDivide.GetParameters(OptimizationType.Nesting).ToListAsync();

            Assert.IsNotNull(parameters);
            Assert.IsTrue(parameters.Any());
            Assert.IsFalse(parameters.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(parameters.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            parameters.Trace();
        }
    }
}