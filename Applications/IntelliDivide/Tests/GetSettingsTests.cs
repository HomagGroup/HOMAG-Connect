using HomagConnect.IntelliDivide.Samples;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests
{
    [TestClass]
    public class GetSettingsTests : IntelliDivideTestBase
    {
        protected Guid TestSubscriptionId { get; } = new("62b8fee0-1b35-41c1-b03a-b947304a0d58");

        [TestMethod]
        public async Task GetCuttingMachines()
        {
            var intelliDivide = GetIntelliDivideClient(TestSubscriptionId);

            await GetSettingsSamples.GetCuttingMachinesSample(intelliDivide);
        }
    }
}