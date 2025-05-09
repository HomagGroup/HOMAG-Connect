﻿using HomagConnect.ProductionManager.Samples.Authentication;

namespace HomagConnect.ProductionManager.Tests.Authentication
{
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Authentication")]
    public class AuthenticationTests : ProductionManagerTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public async Task Authentication_UsingHttpClient()

        {
            await AuthenticationSamples.CreateProductionManagerClientUsingHttpClient(SubscriptionId, AuthorizationKey);
        }

        [TestMethod]
        public async Task Authentication_UsingSubscriptionIdAndToken()
        {
            await AuthenticationSamples.CreateProductionManagerClientUsingSubscriptionIdAndToken(SubscriptionId, AuthorizationKey);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}