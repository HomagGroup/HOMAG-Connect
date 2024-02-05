using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class ParameterSamples
    {
        /// <summary />
        public static async Task GetCuttingParametersSample(IntelliDivideClient intelliDivide)
        {
            var parameters = (await intelliDivide.GetParametersAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(parameters);
            Assert.IsTrue(parameters.Any());
            Assert.IsFalse(parameters.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(parameters.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            parameters.Trace();
        }

        /// <summary />
        public static async Task GetNestingParametersSample(IntelliDivideClient intelliDivide)
        {
            var parameters = (await intelliDivide.GetParametersAsync(OptimizationType.Nesting)).ToArray();

            Assert.IsNotNull(parameters);
            Assert.IsTrue(parameters.Any());
            Assert.IsFalse(parameters.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(parameters.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            parameters.Trace();
        }
    }
}