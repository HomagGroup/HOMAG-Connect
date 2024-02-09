using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;

using Microsoft.Extensions.Configuration;

namespace HomagConnect.IntelliDivide.Tests.Base;

public class IntelliDivideTestBase
{
    public virtual TestContext? TestContext { get; set; }

    protected string BaseUrl
    {
        get
        {
            return GetConfigurationSetting("HomagConnect:BaseUrl");
        }
    }

    protected string SubscriptionId
    {
        get
        {
            var subscriptionId = GetConfigurationSetting($"HomagConnect:IntelliDivide:SubscriptionId");

            Assert.IsFalse(string.IsNullOrWhiteSpace(subscriptionId), "SubscriptionId in appSettings json must not be null or whitespace.");
            Assert.IsTrue(Guid.TryParse(subscriptionId, out var guid), "SubscriptionId in appSettings json must be the subscription id which must be a GUID.");

            return subscriptionId;
        }
    }

    protected string Token
    {
        get
        {
            var token = GetConfigurationSetting("HomagConnect:IntelliDivide:Token");

            Assert.IsFalse(string.IsNullOrWhiteSpace(token), "Token in appSettings json must not be null or whitespace.");

            return token;
        }
    }

    private IConfigurationRoot? Configuration { get; set; }

    protected static string EncodeBase64Token(string username, string token)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{token}"));
    }

    protected IIntelliDivideClient GetIntelliDivideClient()
    {
        Trace($"BaseUrl: {BaseUrl}");
        Trace($"Subscription: {SubscriptionId}");
        Trace($"Token: {Token.Substring(0,4)}*");

        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri(BaseUrl);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId, Token));

        return new IntelliDivideClient(httpClient);
    }

    protected void Trace(string o)
    {
        if (TestContext == null)
        {
            Console.WriteLine(o);
        }
        else
        {
            TestContext.WriteLine(o);
        }
    }

    private string GetConfigurationSetting(string key)
    {
        {
            var config = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);

            if (!string.IsNullOrWhiteSpace(config))
            {
                return config;
            }
        }

        {
            Configuration ??= new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("05d68c42-49ad-4338-91d5-e80d2c675907")
                .Build();

            var config = Configuration[key];

            if (!string.IsNullOrWhiteSpace(config))
            {
                return config;
            }
        }

        throw new ConfigurationErrorsException($"Missing config setting: {key}");
    }
}