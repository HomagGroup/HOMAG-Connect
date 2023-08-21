using System.Net.Http.Headers;

using HomagConnect.MmrMobile.Client.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.MmrMobile.Samples
{
    [TestClass]
    public class GetCountersTest : TestBase
    {
        [TestMethod]
        [TestCategory("UserTestNoInteractionNeeded")]
        public async Task GetCounterTest()
        {
            (var baseUrl, var username, var token) = ReadProps();
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username, token));
            var mmrMobileService = new MmrMobileService(client);

            var counters = await mmrMobileService.GetCounterData(username);

            Assert.IsNotNull(counters);
            Assert.IsTrue(counters.Count() >= 0);
        }
    }
}