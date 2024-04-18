using HomagConnect.IntelliDivide.Samples.Authorization;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Authorization
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Authorization")]
    public class AuthenticationTests : IntelliDivideTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public async Task Authentication_UsingHttpClient()

        {
            await AuthorizationSamples.CreateIntelliDivideClientUsingHttpClient(SubscriptionId, AuthorizationKey);
        }

        [TestMethod]
        public async Task Authentication_UsingSubscriptionIdAndToken()
        {
            await AuthorizationSamples.CreateIntelliDivideClientUsingSubscriptionIdAndToken(SubscriptionId, AuthorizationKey);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}