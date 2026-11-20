using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionAssist.Client;

using Shouldly;

namespace HomagConnect.ProductionAssist.Tests;

[TestClass]
public class RateLimitRetryClientTests
{
    [TestMethod]
    [UnitTest("ProductionAssist")]
    public void RetryOnRateLimit_IsPassedToAllSubClients()
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new ProductionAssistClient(httpClient);

        client.RetryOnRateLimit.ShouldBeFalse();
        client.Cutting.RetryOnRateLimit.ShouldBeFalse();

        client.RetryOnRateLimit = true;

        client.Cutting.RetryOnRateLimit.ShouldBeTrue();
        client.Nesting.RetryOnRateLimit.ShouldBeTrue();
        client.Feedback.RetryOnRateLimit.ShouldBeTrue();
    }
}
