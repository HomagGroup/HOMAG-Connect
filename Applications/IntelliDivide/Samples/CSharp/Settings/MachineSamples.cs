using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class MachineSamples
    {
        /// <summary>
        /// The example shows how to get the defined machines of type cutting.
        /// </summary>
        public static async Task Settings_GetCuttingMachinesSample(IIntelliDivideClient intelliDivide)
        {
            var machines = await intelliDivide.GetMachines(OptimizationType.Cutting).ToListAsync();
            if (machines == null || machines.Count <= 0)
            {
                Assert.Fail("No cutting machines could be found.");
            }

            machines.Trace();
        }

        /// <summary>
        /// The example shows how to get the defined machines independent of their type.
        /// </summary>
        public static async Task Settings_GetMachinesSample(IIntelliDivideClient intelliDivide)
        {
            var machines = await intelliDivide.GetMachines().ToListAsync();
            if (machines == null || machines.Count <= 0)
            {
                Assert.Fail("No machines could be found.");
            }

            machines.Trace();
        }

        /// <summary>
        /// The example shows how to read the machine parameters of a cutting machine
        /// (sections In general, Offcuts, Trim cuts, Tension trims, Book height and Allocations).
        /// </summary>
        public static async Task Settings_GetCuttingMachineParametersSample(IIntelliDivideClient intelliDivide, string machineName)
        {
            var machineDetails = await intelliDivide.GetMachineDetails(machineName);
            if (machineDetails?.CuttingParameters == null)
            {
                Assert.Fail($"No cutting parameters could be found for machine '{machineName}'.");
            }

            var parameters = machineDetails.CuttingParameters;

            // Sections and properties which aren't available for the machine or the license are null and omitted.
            parameters.Trace();
        }

        /// <summary>
        /// The example shows how to get the defined machines of type nesting.
        /// </summary>
        public static async Task Settings_GetNestingMachinesSample(IIntelliDivideClient intelliDivide)
        {
            const OptimizationType optimizationType = OptimizationType.Nesting;

            var machines = await intelliDivide.GetMachines(optimizationType).ToListAsync();
            if (machines == null || machines.Count <= 0)
            {
                Assert.Fail("No nesting machines could be found.");
            }

            machines.Trace();
        }
    }
}