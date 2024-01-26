using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples
{
    public class GetSettingsSamples
    {
        public static async Task GetCuttingMachinesSample(IntelliDivideClient intelliDivide)
        {
            var machines = (await intelliDivide.GetMachinesAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(machines);
            Assert.IsTrue(machines.Any());
            Assert.IsFalse(machines.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(machines.Any(m => string.IsNullOrWhiteSpace(m.Name)));
        }

        public static async Task GetNestingMachinesSample(IntelliDivideClient intelliDivide)
        {
            const OptimizationType optimizationType = OptimizationType.Nesting;

            var machines = (await intelliDivide.GetMachinesAsync(optimizationType)).ToArray();

            Assert.IsNotNull(machines);
            Assert.IsTrue(machines.Any());
            Assert.IsFalse(machines.Any(m => m.OptimizationType != optimizationType));
            Assert.IsFalse(machines.Any(m => string.IsNullOrWhiteSpace(m.Name)));
        }

        public static async Task GetCuttingParametersSample(IntelliDivideClient intelliDivide)
        {
            var parameters = (await intelliDivide.GetParametersAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(parameters);
            Assert.IsTrue(parameters.Any());
            Assert.IsFalse(parameters.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(parameters.Any(m => string.IsNullOrWhiteSpace(m.Name)));
        }

        public static async Task GetNestingParametersSample(IntelliDivideClient intelliDivide)
        {
            var parameters = (await intelliDivide.GetParametersAsync(OptimizationType.Nesting)).ToArray();

            Assert.IsNotNull(parameters);
            Assert.IsTrue(parameters.Any());
            Assert.IsFalse(parameters.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(parameters.Any(m => string.IsNullOrWhiteSpace(m.Name)));
        }

        public static async Task GetCuttingTemplatesSample(IntelliDivideClient intelliDivide)
        {
            var templates = (await intelliDivide.GetImportTemplatesAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));
        }

        public static async Task GetNestingTemplatesSample(IntelliDivideClient intelliDivide)
        {
            var templates = (await intelliDivide.GetImportTemplatesAsync(OptimizationType.Nesting)).ToArray();

            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));
        }
    }
}
