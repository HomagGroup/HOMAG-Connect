using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Samples.Results;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Results;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Results")]
public class OptimizationResultTests : IntelliDivideTestBase
{
#pragma warning disable S2699 // Tests should include assertions
    /// <summary />
    [TestMethod]
    public async Task Optimization_GetResult_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimization_GetStatus_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationStatusSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimization_HttpClientAutoCreated_GetResult_NoException()
    {
        var intelliDivide = new IntelliDivideClient(SubscriptionId, AuthorizationKey, BaseUrl);

        await OptimizationResultSamples.GetOptimizationSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_FilterStatus_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsHavingStatusOptimized(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_FilterType_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsOfTypeCuttingSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_List_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationsSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Solution_GetDetails_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionDetailsSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Solution_List_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionsSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Solution_ProducedParts()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionPartsSample(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2024, 4, 15)]
    public async Task Solution_Patterns()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionPatternsSample(intelliDivide);
    }
#pragma warning restore S2699 // Tests should include assertions
}