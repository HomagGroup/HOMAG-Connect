using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Samples.Optimizations;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Optimizations;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Optimizations")]
public class OptimizationsRetrievalTests : IntelliDivideTestBase
{
#pragma warning disable S2699 // Tests should include assertions

    /// <summary />
    [TestMethod]
    public async Task Optimizations_GetFirstFiveOptimizations()
    {
        var intelliDivide = new IntelliDivideClient(SubscriptionId, AuthorizationKey, BaseUrl);

        await OptimizationRetrievalSamples.Optimizations_GetFirstFiveOptimizations(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_GetOptimizationsWithStatusOptimized()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationRetrievalSamples.Optimizations_GetOptimizationsWithStatusOptimized(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_GetOptimizationsOfTypeCutting()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationRetrievalSamples.Optimizations_GetOptimizationsOfTypeCutting(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_GetSolutionsAndSolutionDetails()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationRetrievalSamples.Optimizations_GetSolutionsAndSolutionDetails(intelliDivide);
    }

    /// <summary />
    [TestMethod]
    public async Task Optimizations_ArchiveAndDelete()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationRetrievalSamples.Optimizations_ArchiveAndDelete(intelliDivide);
    }
    
    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 7, 15, "DF-Optimization")]
    public async Task Optimizations_GetExportsForSolution()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationRetrievalSamples.Optimizations_GetExportsForSolution(intelliDivide);
    }

#pragma warning restore S2699 // Tests should include assertions
}