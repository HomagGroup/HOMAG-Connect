using System.Text;

using Microsoft.Extensions.Configuration;

namespace HomagConnect.MmrMobile.Samples
{
    public class TestBase
    {
        public static (string, string, string) ReadProps()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            var baseUrl = configuration["HomagApiGateway:BaseUrl"];
            var username = configuration["HomagApiGateway:Username"];
            var token = configuration["HomagApiGateway:Token"];

            return (baseUrl, username, token);
        }

        public static string EncodeBase64Token(string username, string token)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{token}"));
        }
    }
}