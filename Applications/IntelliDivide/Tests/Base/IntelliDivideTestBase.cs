using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

using HomagConnect.IntelliDivide.Client;

using Microsoft.Extensions.Configuration;

namespace HomagConnect.IntelliDivide.Tests.Base;

public class IntelliDivideTestBase
{
    public virtual TestContext? TestContext { get; set; }

    protected static string EncodeBase64Token(string username, string token)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{token}"));
    }

    protected static IntelliDivideClient GetIntelliDivideClient()
    {
        var (baseUrl, subscriptionId, token) = ReadProps("IntelliDivide");

        Assert.IsFalse(string.IsNullOrWhiteSpace(baseUrl), "BaseUrl in appSettings json must not be null or whitespace.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(subscriptionId), "SubscriptionId in appSettings json must not be null or whitespace.");
        Assert.IsTrue(Guid.TryParse(subscriptionId, out var guid), "SubscriptionId in appSettings json must be the subscription id which must be a GUID.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(token), "Token in appSettings json must not be null or whitespace.");

        Debug.WriteLine($"Base:\t{baseUrl}");
        Debug.WriteLine($"Subscription:\t{subscriptionId}");
        Debug.WriteLine("");

        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(subscriptionId, token));

        return new IntelliDivideClient(httpClient);
    }

    protected static (string? baseUrl, string? subscriptionId, string? token) ReadProps(string area)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets("05d68c42-49ad-4338-91d5-e80d2c675907")
            .Build();

        var baseUrl = configuration["HomagConnect:BaseUrl"];
        var subscriptionId = configuration[$"HomagConnect:{area}:SubscriptionId"];
        var token = configuration[$"HomagConnect:{area}:Token"];

        return (baseUrl, subscriptionId, token);
    }
}