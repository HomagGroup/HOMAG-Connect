using System.Net.Http.Headers;
using System.Text;

using HomagConnect.IntelliDivide.Client;

using Microsoft.Extensions.Configuration;

namespace HomagConnect.IntelliDivide.Tests.Base;

public class IntelliDivideTestBase
{
    protected static string EncodeBase64Token(string username, string token)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{token}"));
    }

    protected static IntelliDivideClient GetIntelliDivideClient(Guid subscriptionId)
    {
        var (baseUrl, username, token) = ReadProps("IntelliDivide");

        Assert.IsFalse(string.IsNullOrWhiteSpace(baseUrl), "BaseUrl in appSettings json must not be null or whitespace.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(username), "Username in appSettings json must not be null or whitespace.");
        Assert.IsTrue(Guid.TryParse(username, out subscriptionId), "Username in appSettings json must be the subscription id which must be a GUID.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(token), "Token in appSettings json must not be null or whitespace.");

        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username, token));

        return new IntelliDivideClient(httpClient, subscriptionId);
    }

    protected static (string? baseUrl, string? username, string? token) ReadProps(string area)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
        var baseUrl = configuration["HomagConnect:BaseUrl"];
        var username = configuration[$"HomagConnect:{area}:Username"];
        var token = configuration[$"HomagConnect:{area}:Token"];

        return (baseUrl, username, token);
    }
}