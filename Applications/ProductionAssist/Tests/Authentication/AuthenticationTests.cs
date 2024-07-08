using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionAssist.Samples.Authentication;

namespace HomagConnect.ProductionAssist.Tests.Authentication
{
    [TestClass]
    [TestCategory("ProductionAssist")]
    [TestCategory("ProductionAssist.Authentication")]
    [TemporaryDisabledOnServer(2024, 7, 22)]
    public class AuthenticationTests : ProductionAssistTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public async Task Authentication_UsingHttpClient()
        {
            await AuthenticationSamples.CreateProductionAssistClientUsingHttpClient(SubscriptionId, AuthorizationKey);
        }

        [TestMethod]
        public async Task Authentication_UsingSubscriptionIdAndToken()
        {
            await AuthenticationSamples.CreateProductionAssistClientUsingSubscriptionIdAndToken(SubscriptionId, AuthorizationKey);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}
