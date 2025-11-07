using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class ParameterSamples
    {
        /// <summary>
        /// The example shows how to get the defined parameters of type cutting.
        /// </summary>
        public static async Task Settings_GetCuttingParametersSample(IIntelliDivideClient intelliDivide)
        {
            var parameters = await intelliDivide.GetParameters(OptimizationType.Cutting).ToListAsync();
            if (parameters == null || !parameters.Any())
            {
                Assert.Inconclusive("No cutting parameters could be found.");
            }

            parameters.Trace();
        }

        /// <summary>
        /// The example shows how to get the defined parameters of type nesting.
        /// </summary>
        public static async Task GetNestingParametersSample(IIntelliDivideClient intelliDivide)
        {
            var parameters = await intelliDivide.GetParameters(OptimizationType.Nesting).ToListAsync();
            if (parameters == null || !parameters.Any())
            {
                Assert.Inconclusive("No nesting parameters could be found.");
            }

            parameters.Trace();
        }
    }
}