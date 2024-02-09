using HomagConnect.IntelliDivide.Tests.Base;

using Microsoft.Extensions.Configuration;

namespace HomagConnect.IntelliDivide.Tests;

[TestClass]
public class GetTestContextTests : IntelliDivideTestBase
{
    public override TestContext? TestContext { get; set; }

    [TestMethod]
    public void GetBaseUrlFromAppSettings()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets("05d68c42-49ad-4338-91d5-e80d2c675907")
            .Build();

        var baseUrl = configuration["HomagConnect:BaseUrl"];

        if (TestContext == null)
        {
            Console.Write($"BaseUrl: {baseUrl}");
        }
        else
        {
            TestContext.WriteLine($"BaseUrl: {baseUrl}");
        }
    }

    [TestMethod]
    public void GetBaseUrlFromContext()
    {
        if (TestContext == null)
        {
          
        }
        else
        {
            var baseUrl = TestContext.Properties["BaseUrl"];

            TestContext.WriteLine($"BaseUrl: {baseUrl}");
        }
    }

    [TestMethod]
    public void GetBaseUrlFromEnvironment()
    {
        var baseUrl = Environment.GetEnvironmentVariable("BaseUrl", EnvironmentVariableTarget.Process);

        if (TestContext == null)
        {
            Console.Write($"BaseUrl: {baseUrl}");
        }
        else
        {
            TestContext.WriteLine($"BaseUrl: {baseUrl}");
        }
    }
}