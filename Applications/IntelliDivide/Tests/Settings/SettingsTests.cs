using FluentAssertions;

using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Samples.Settings;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Settings
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Settings")]
    public class SettingsTests : IntelliDivideTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public void IntelliDivide_CheckConfiguration_ConfigValid()
        {
            BaseUrl.Should().NotBeNull();
            SubscriptionId.Should().NotBeEmpty();
            AuthorizationKey.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task Settings_GetCuttingMachines()

        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.Settings_GetCuttingMachinesSample(intelliDivide);
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