using HomagConnect.IntelliDivide.Samples.Authentication;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Authentication
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Authentication")]
    public class AuthenticationTests : IntelliDivideTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public async Task Authentication_UsingHttpClient()

        {
            await AuthenticationSamples.CreateIntelliDivideClientUsingHttpClient(SubscriptionId, AuthorizationKey);
        }

        [TestMethod]
        public async Task Authentication_UsingSubscriptionIdAndToken()
        {
            await AuthenticationSamples.CreateIntelliDivideClientUsingSubscriptionIdAndToken(SubscriptionId, AuthorizationKey);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}