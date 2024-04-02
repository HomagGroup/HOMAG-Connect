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
    public static (string? baseUrl, string? username, string? token) ReadProps()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets("10225a60-2f4f-4e77-b6b5-b57926da5ad6")
            .Build();
        var baseUrl = configuration["HomagConnect:BaseUrl"];
        var username = configuration[$"HomagConnect:SubscriptionId"];
        var AuthorizationKey = configuration[$"HomagConnect:AuthorizationKey"];

        return (baseUrl, username, AuthorizationKey);
    }
}