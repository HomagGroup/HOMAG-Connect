using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class MachineSamples
    {
        /// <summary />
        public static async Task GetCuttingMachinesSample(IIntelliDivideClient intelliDivide)
        {
            var machines = (await intelliDivide.GetMachinesAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(machines);
            Assert.IsTrue(machines.Any());
            Assert.IsFalse(machines.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(machines.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            machines.Trace();
        }

        /// <summary />
        public static async Task GetMachinesSample(IIntelliDivideClient intelliDivide)
        {
            var machines = (await intelliDivide.GetMachinesAsync()).ToArray();

            Assert.IsNotNull(machines);
            Assert.IsTrue(machines.Any());
            Assert.IsFalse(machines.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            machines.Trace();
        }

        /// <summary />
        public static async Task GetNestingMachinesSample(IIntelliDivideClient intelliDivide)
        {
            const OptimizationType optimizationType = OptimizationType.Nesting;

            var machines = (await intelliDivide.GetMachinesAsync(optimizationType)).ToArray();

            Assert.IsNotNull(machines);
            Assert.IsTrue(machines.Any());
            Assert.IsFalse(machines.Any(m => m.OptimizationType != optimizationType));
            Assert.IsFalse(machines.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            machines.Trace();
        }
    }
}