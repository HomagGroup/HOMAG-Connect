using System.Text;

using Microsoft.Extensions.Configuration;

namespace HomagConnect.MmrMobile.Samples.Tests
{
    public class TestBase
    {
        public static (string, string, string) ReadProps(string area)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            var baseUrl = configuration["HomagConnect:BaseUrl"];
            var username = configuration[$"HomagConnect:{area}:SubscriptionId"];
            var token = configuration[$"HomagConnect:{area}:Token"];

            return (baseUrl, username, token);
        }

        public static string EncodeBase64Token(string username, string token)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{token}"));
        }
    }
}