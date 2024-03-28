using HomagConnect.Base.Tests;
using HomagConnect.Base.Tests.Attributes;
using HomagConnect.MmrMobile.Client;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace HomagConnect.MmrMobile.Tests
{
    [TestClass]
    public class GetMmrRoutes : MmrTestBase
    {
        [TestMethod]
        [TemporaryDisabledOnServer(2024, 4, 1)]
        public async Task GetCounterTest()
        {
            var (baseUrl, username, token) = ReadProps("MmrMobile");

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? "")
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username ?? "", token ?? ""));
            var mmrMobileClient = new MmrMobileClient(client);

            var counters = await mmrMobileClient.GetCounterData();

            Assert.IsNotNull(counters);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 4, 1)]
        public async Task GetStatesTest()
        {
            var (baseUrl, username, token) = ReadProps("MmrMobile");

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? "")
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username ?? "", token ?? ""));
            var mmrMobileClient = new MmrMobileClient(client);

            var states = await mmrMobileClient.GetStateData();

            Assert.IsNotNull(states);
        }
    }
}