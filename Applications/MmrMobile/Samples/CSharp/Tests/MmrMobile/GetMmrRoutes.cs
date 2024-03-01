using System.Net.Http.Headers;

using HomagConnect.Base.Tests.Attributes;
using HomagConnect.MmrMobile.Client.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.MmrMobile.Samples.Tests.MmrMobile
{
    [TestClass]
    public class GetMmrRoutes : TestBase
    {
        [TestMethod]
        [TemporaryDisabledOnServer(2024, 4, 1)]
        public async Task GetCounterTest()
        {
            (var baseUrl, var username, var token) = ReadProps("MmrMobile");
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username, token));
            var mmrMobileService = new MmrMobileService(client);

            var counters = await mmrMobileService.GetCounterData();

            Assert.IsNotNull(counters);
            Assert.IsTrue(counters.Count() >= 0);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 4, 1)]
        public async Task GetStatesTest()
        {
            (var baseUrl, var username, var token) = ReadProps("MmrMobile");
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username, token));
            var mmrMobileService = new MmrMobileService(client);

            var counters = await mmrMobileService.GetStateData();

            Assert.IsNotNull(counters);
            Assert.IsTrue(counters.Count() >= 0);
        }
    }
}