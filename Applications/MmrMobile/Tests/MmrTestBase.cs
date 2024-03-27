using HomagConnect.Base.Tests;
using Microsoft.Extensions.Configuration;

namespace HomagConnect.MmrMobile.Tests;

/// <summary>
/// 
/// </summary>
public class MmrTestBase : TestBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="area"></param>
    /// <returns></returns>
    public static (string? baseUrl, string? username, string? token) ReadProps(string area)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets("585739b5-e918-437e-a0e6-61b90a4be38d")
            .Build();
        var baseUrl = configuration["HomagConnect:BaseUrl"];
        var username = configuration[$"HomagConnect:{area}:SubscriptionId"];
        var token = configuration[$"HomagConnect:{area}:Token"];

        return (baseUrl, username, token);
    }
}