using FluentAssertions;
using HomagConnect.Base.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomagConnect.Base.Tests.Services
{
    [TestClass]
    public class ServiceBaseTests
    {
        [TestMethod]
        public async Task TestBase()
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo("de-DE");
            const string getPath = "/headers/acceptLanguage";
            using var client = new TestApplication(async ctx =>
            {
                var result = new OkObjectResult(ctx.Request.Headers.AcceptLanguage.ToString());
                return result;
            }, getPath).CreateClient();

            var service = new MyTestService(client);
            var response = await service.Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, getPath));
            Assert.IsNotNull(response);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("de-DE");
        }
    }

    public class MyTestService : ServiceBase
    {
        public MyTestService(HttpClient client) : base(client)
        {
        }
    }
}
