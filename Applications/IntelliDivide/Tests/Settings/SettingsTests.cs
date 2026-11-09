using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;

using Shouldly;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Settings;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Settings
{
    [TestClass]
    [DeploymentTest("IntelliDivide.Settings")]
    public class SettingsTests : IntelliDivideTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public void IntelliDivide_CheckConfiguration_ConfigValid()
        {
            BaseUrl.ShouldNotBeNull();
            SubscriptionId.ShouldNotBe(Guid.Empty);
            AuthorizationKey.ShouldNotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task Settings_GetCuttingMachines()

        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.Settings_GetCuttingMachinesSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetMachineDetails_CreateAndDeleteMachine()
        {
            var intelliDivide = GetIntelliDivideClient();
            var machine = (await intelliDivide.GetMachines(OptimizationType.Cutting).ToListAsync())
                .FirstOrDefault(machine =>
                    machine.Name.Contains("S-100", StringComparison.OrdinalIgnoreCase)
                    || machine.Name.Contains("S-200", StringComparison.OrdinalIgnoreCase)
                    || machine.Name.Contains("S-300", StringComparison.OrdinalIgnoreCase)
                    || machine.Name.Contains("S-400", StringComparison.OrdinalIgnoreCase));

            if (machine == null)
            {
                Assert.Inconclusive("No cutting machine with model S-100, S-200, S-300, or S-400 is available.");
            }

            machine.ShouldNotBeNull();

            var machineDetails = await intelliDivide.GetMachineDetails(machine.Name);

            machineDetails.ShouldNotBeNull();
            machineDetails.Name = $"HOMAG Connect Test {Guid.NewGuid():N}";

            var createdMachine = await intelliDivide.CreateMachine(machineDetails);

            try
            {
                createdMachine.Name.ShouldBe(machineDetails.Name);
            }
            finally
            {
                await intelliDivide.DeleteMachine(createdMachine.Name);
            }
        }

        [TestMethod]
        public async Task Settings_GetCuttingParameters()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ParameterSamples.Settings_GetCuttingParametersSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetCuttingTemplates()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ImportTemplatesSamples.GetCuttingTemplatesSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetMachines()
        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.Settings_GetMachinesSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetNestingMachines()
        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.Settings_GetNestingMachinesSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetNestingParameters()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ParameterSamples.GetNestingParametersSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetNestingTemplates()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ImportTemplatesSamples.GetNestingTemplatesSample(intelliDivide);
        }

        [TestMethod]
        public async Task Settings_GetPartProperties()
        {
            var intelliDivide = GetIntelliDivideClient();

            await GeneralSettingsSamples.GetPartPropertiesSample(intelliDivide);
        }
    }

#pragma warning restore S2699 // Tests should include assertions
}