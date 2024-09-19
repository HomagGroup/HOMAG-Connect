﻿using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

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

            machines.Trace();
        }

        /// <summary>
        /// The example shows how to get the defined machines independent of their type.
        /// </summary>
        public static async Task Settings_GetMachinesSample(IIntelliDivideClient intelliDivide)
        {
            var machines = await intelliDivide.GetMachines().ToListAsync();

            machines.Trace();
        }

        /// <summary>
        /// The example shows how to get the defined machines of type nesting.
        /// </summary>
        public static async Task Settings_GetNestingMachinesSample(IIntelliDivideClient intelliDivide)
        {
            const OptimizationType optimizationType = OptimizationType.Nesting;

            var machines = await intelliDivide.GetMachines(optimizationType).ToListAsync();

            machines.Trace();
        }
    }
}