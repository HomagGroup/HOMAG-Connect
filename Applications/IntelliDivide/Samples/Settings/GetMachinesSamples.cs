using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    public class MachineSamples
    {
        public static async Task GetCuttingMachinesSample(IntelliDivideClient intelliDivide)
        {
            var machines = (await intelliDivide.GetMachinesAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(machines);
            Assert.IsTrue(machines.Any());
            Assert.IsFalse(machines.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(machines.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            machines.Trace();
        }

        public static async Task GetNestingMachinesSample(IntelliDivideClient intelliDivide)
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