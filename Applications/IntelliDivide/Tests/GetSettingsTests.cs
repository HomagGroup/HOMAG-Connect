using HomagConnect.IntelliDivide.Samples.Settings;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests
{
    [TestClass]
    [TestCategory("IntelliDivide.Settings")]
    [TestCategory("UserTestInteractionNeeded")]
    public class GetSettingsTests : IntelliDivideTestBase
    {
        public TestContext TestContext { get; set; }

        [TestMethod] public void GetBaseUrlFromContext()
        {
            TestContext.WriteLine("BaseUrl:" +  TestContext.Properties["BaseUrl"]);
        }

        [TestMethod]
        public async Task GetCuttingMachines()
        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.GetCuttingMachinesSample(intelliDivide);
        }

        [TestMethod]
        public async Task GetCuttingParameters()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ParameterSamples.GetCuttingParametersSample(intelliDivide);
        }

        [TestMethod]
        public async Task GetCuttingTemplates()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ImportTemplatesSamples.GetCuttingTemplatesSample(intelliDivide);
        }

        [TestMethod]
        public async Task GetMachines()
        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.GetMachinesSample(intelliDivide);
        }

        [TestMethod]
        public async Task GetNestingMachines()
        {
            var intelliDivide = GetIntelliDivideClient();

            await MachineSamples.GetNestingMachinesSample(intelliDivide);
        }

        [TestMethod]
        public async Task GetNestingParameters()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ParameterSamples.GetNestingParametersSample(intelliDivide);
        }

        [TestMethod]
        public async Task GetNestingTemplates()
        {
            var intelliDivide = GetIntelliDivideClient();

            await ImportTemplatesSamples.GetNestingTemplatesSample(intelliDivide);
        }
    }
}