using HomagConnect.IntelliDivide.Tests.Base;
using Microsoft.Extensions.Configuration;

namespace HomagConnect.IntelliDivide.Tests;

[TestClass]
public class GetTestContextTests : IntelliDivideTestBase
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public void GetBaseUrlFromContext()
    {
        TestContext.WriteLine("BaseUrl:" + TestContext.Properties["BaseUrl"]);
    }

    [TestMethod]
    public void GetBaseUrlFromEnvironment()
    {
        TestContext.WriteLine("BaseUrl:" + System.Environment.GetEnvironmentVariable("BaseUrl"));
    }

    [TestMethod]
    public void GetBaseUrlFromAppSettings()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets("05d68c42-49ad-4338-91d5-e80d2c675907")
            .Build();

        var baseUrl = configuration["HomagConnect:BaseUrl"];

        TestContext.WriteLine("BaseUrl:" + baseUrl);
    }


}