using System.Net.Http.Headers;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.IntelliDivide.Tests.Base;

public class IntelliDivideTestBase : TestBase
{
    /// <summary>
    /// Checks if the test material codes exist.
    /// </summary>
    protected async Task EnsureSampleMaterialCodesExists(string sampleMaterialCodeGrainLengthwise, string sampleMaterialCodeGrainNone)
    {
        var materialManagerClient = GetMaterialManagerClient();

        IList<BoardType> boardTypesByMaterialCodes;

        try
        {
            boardTypesByMaterialCodes = await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes(new[] { sampleMaterialCodeGrainLengthwise, sampleMaterialCodeGrainNone })
                .ToListAsync();
        }
        catch (ProblemDetailsException ex)
        {
            if (ex.Message.Contains("No board types found."))
            {
                boardTypesByMaterialCodes = new List<BoardType>();
            }
            else
            {
                throw;
            }
        }

        if (boardTypesByMaterialCodes.All(b => b.MaterialCode != sampleMaterialCodeGrainLengthwise))
        {
            await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
            {
                MaterialCode = sampleMaterialCodeGrainLengthwise,
                BoardCode = $"{sampleMaterialCodeGrainLengthwise}_2800_2070",
                Thickness = 19.0,
                Grain = Grain.Lengthwise,
                Width = 2070,
                Length = 2800,
                Type = BoardTypeType.Board,
                CoatingCategory = CoatingCategory.Undefined,
                MaterialCategory = BoardMaterialCategory.Undefined
            });
        }

        if (boardTypesByMaterialCodes.All(b => b.MaterialCode != sampleMaterialCodeGrainNone))
        {
            await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
            {
                MaterialCode = sampleMaterialCodeGrainNone,
                BoardCode = $"{sampleMaterialCodeGrainNone}_2800_2070",
                Thickness = 19.0,
                Grain = Grain.None,
                Width = 2070,
                Length = 2800,
                Type = BoardTypeType.Board,
                CoatingCategory = CoatingCategory.Undefined,
                MaterialCategory = BoardMaterialCategory.Undefined
            });
        }
    }

    protected static async Task EnsureImportTemplateExists(IIntelliDivideClient intelliDivide, OptimizationType optimizationType, string importTemplateName)
    {
        var optimizationImportTemplates = await intelliDivide.GetImportTemplates(optimizationType).ToListAsync();

        if (optimizationImportTemplates.All(t => t.Name != importTemplateName))
        {
            Assert.Inconclusive($"The import template '{importTemplateName}' does not exist.");
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

    /// <summary>
    /// Gets a new instance of the <see cref="MaterialManagerClient" />.
    /// </summary>
    protected MaterialManagerClient GetMaterialManagerClient()
    {
        Trace($"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey.Substring(0, 4)}*");

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new MaterialManagerClient(httpClient);
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
            var startedOptimization = await intelliDivideClient.GetOptimizations(optimizationType, OptimizationStatus.Started, parallelOptimizationsRunningLimit + 1).ToListAsync();

            if (startedOptimization.Count < parallelOptimizationsRunningLimit)
            {
                return;
            }

            await Task.Delay(5000);
        }

        Assert.Fail("WaitForStartedOptimizationsToComplete has timed out.");
    }
}