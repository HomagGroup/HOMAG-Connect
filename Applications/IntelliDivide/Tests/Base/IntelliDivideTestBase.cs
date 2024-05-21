using System.Net.Http.Headers;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Tests;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Tests.Base;

public class IntelliDivideTestBase : TestBase
{
    protected override Guid UserSecretsFolder { get; set; } = new("05d68c42-49ad-4338-91d5-e80d2c675907");

    /// <summary>
    /// Checks if the sample material exists.
    /// </summary>
    protected async Task EnsureSampleMaterialExists(IEnumerable<string> materialCodes)
    {
        var distinctMaterialCodes = materialCodes.Distinct().ToList();
        var materialManagerClient = GetMaterialManagerClient();
        var boardTypes = await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes(distinctMaterialCodes).ToListAsync();

        Assert.IsNotNull(boardTypes, "Test material missing.");
        Assert.IsTrue(boardTypes.Any(), "Test material missing.");

        foreach (var materialCode in distinctMaterialCodes)
        {
            if (!boardTypes.Any(b => string.Equals(b.MaterialCode, materialCode, StringComparison.InvariantCultureIgnoreCase)))
            {
                Assert.Fail($"Test material '{materialCode} is missing.");
            }
        }
    }

    protected IIntelliDivideClient GetIntelliDivideClient()
    {
        Trace($"BaseUrl: {BaseUrl}");
        Trace($"Subscription: {SubscriptionId}");
        Trace($"AuthorizationKey: {AuthorizationKey.Substring(0, 4)}*");

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

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

    /// <summary>
    /// Waits until no more optimizations are running.
    /// </summary>
    protected async Task WaitForParallelRunningOptimizationsWithinLimit(OptimizationType optimizationType, TimeSpan timeout)
    {
        var startTime = DateTime.Now;
        const int parallelOptimizationsRunningLimit = 2;
        var intelliDivideClient = GetIntelliDivideClient();

        while (startTime.Add(timeout) > DateTime.Now)
        {
            var startedOptimization = await intelliDivideClient.GetOptimizationsAsync(optimizationType, OptimizationStatus.Started, parallelOptimizationsRunningLimit + 1).ToListAsync();

            if (startedOptimization.Count <= parallelOptimizationsRunningLimit)
            {
                return;
            }

            await Task.Delay(1000);
        }

        Assert.Fail("WaitForStartedOptimizationsToComplete has timed out.");
    }
}