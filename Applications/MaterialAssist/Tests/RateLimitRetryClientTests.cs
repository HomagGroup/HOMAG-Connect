using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Client;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests;

[TestClass]
public class RateLimitRetryClientTests
{
    [TestMethod]
    [UnitTest("MaterialAssist")]
    public void RetryOnRateLimit_IsPassedToAllSubClients()
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri("https://connect.example.com/") };
        var client = new MaterialAssistClient(httpClient);

        client.RetryOnRateLimit.ShouldBeFalse();
        client.Boards.RetryOnRateLimit.ShouldBeFalse();
        client.Edgebands.RetryOnRateLimit.ShouldBeFalse();

        client.RetryOnRateLimit = true;

        client.Boards.RetryOnRateLimit.ShouldBeTrue();
        client.Edgebands.RetryOnRateLimit.ShouldBeTrue();
    }
}
