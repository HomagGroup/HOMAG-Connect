using HomagConnect.MaterialAssist.Samples.Authentication;

namespace HomagConnect.MaterialAssist.Tests.Authentication
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Authentication")]
    public class AuthenticationTests : MaterialAssistTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public async Task Authentication_UsingHttpClient()

        {
            await AuthenticationSamples.CreateMaterialAssistClientUsingHttpClient(SubscriptionId, AuthorizationKey);
        }

        [TestMethod]
        public async Task Authentication_UsingSubscriptionIdAndToken()
        {
            await AuthenticationSamples.CreateMaterialAssistClientUsingSubscriptionIdAndToken(SubscriptionId, AuthorizationKey);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}