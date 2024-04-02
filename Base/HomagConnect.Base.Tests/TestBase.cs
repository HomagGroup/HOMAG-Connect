using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace HomagConnect.Base.Tests
{
    public class TestBase
    {
        private static readonly JsonSerializerSettings _JsonSerializerSettings = new()
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public virtual TestContext? TestContext { get; set; }

        protected string AuthorizationKey
        {
            get
            {
                var authorizationKey = GetConfigurationSetting("HomagConnect:AuthorizationKey");

                Assert.IsFalse(string.IsNullOrWhiteSpace(authorizationKey), "AuthorizationKey in appSettings json must not be null or whitespace.");

                return authorizationKey;
            }
        }

        protected Uri? BaseUrl
        {
            get
            {
                var baseUrl = GetConfigurationSetting("HomagConnect:BaseUrl");

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    return null;
                }

                return new Uri(baseUrl);
            }
        }

        protected Guid SubscriptionId
        {
            get
            {
                var subscriptionId = GetConfigurationSetting($"HomagConnect:SubscriptionId");

                Assert.IsFalse(string.IsNullOrWhiteSpace(subscriptionId), "SubscriptionId in appSettings json must not be null or whitespace.");
                Assert.IsTrue(Guid.TryParse(subscriptionId, CultureInfo.InvariantCulture, out var guid), "SubscriptionId in appSettings json must be the subscription id which must be a GUID.");

                return guid;
            }
        }

        protected virtual Guid UserSecretsFolder { get; set; }

        private IConfigurationRoot? Configuration { get; set; }

        protected static string EncodeBase64Token(string username, string token)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{token}"));
        }

        protected string GetConfigurationSetting(string key)
        {
            var config = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);

            if (config != null)
            {
                return config;
            }

            Configuration ??= new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(UserSecretsFolder.ToString())
                .Build();

            config = Configuration[key];

            if (config != null)
            {
                return config;
            }

            throw new ConfigurationErrorsException($"Missing config setting: {key}");
        }

        protected void Trace(IEnumerable enumerable, [CallerMemberName] string description = "")
        {
            if (TestContext == null)
            {
                Console.WriteLine(description);
                Console.WriteLine(JsonConvert.SerializeObject(enumerable, _JsonSerializerSettings));
                Console.WriteLine(string.Empty);
            }
            else
            {
                TestContext.WriteLine(description);
                TestContext.WriteLine(JsonConvert.SerializeObject(enumerable, _JsonSerializerSettings));
                TestContext.WriteLine(string.Empty);
            }
        }

        protected void Trace(object o, [CallerMemberName] string description = "")
        {
            if (TestContext == null)
            {
                Console.WriteLine(description);
                Console.WriteLine(JsonConvert.SerializeObject(o, _JsonSerializerSettings));
                Console.WriteLine(string.Empty);
            }
            else
            {
                TestContext.WriteLine(description);
                TestContext.WriteLine(JsonConvert.SerializeObject(o, _JsonSerializerSettings));
                TestContext.WriteLine(string.Empty);
            }
        }
    }
}