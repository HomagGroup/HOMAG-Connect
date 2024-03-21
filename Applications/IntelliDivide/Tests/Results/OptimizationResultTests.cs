using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Samples.Results;
using HomagConnect.IntelliDivide.Tests.Base;
using System;

namespace HomagConnect.IntelliDivide.Tests.Results;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Results")]
public class OptimizationResultTests : IntelliDivideTestBase
{
    /// <summary />
    [TestMethod]
    public async Task Optimization_GetResult_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetOptimizationSample(intelliDivide);
    }


    /// <summary />
    [TestMethod]
    public async Task Optimization_HttpClientAutoCreated_GetResult_NoException()
    {
        var intelliDivide = new IntelliDivideClient(SubscriptionId, Token, BaseUrl);

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
    public async Task Statistics_GetMaterial_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialStatisticsAsync = await intelliDivide.GetMaterialStatisticsAsync( DateTime.Now.AddDays(-91), DateTime.Now.AddDays(-1), 100);

        Assert.IsNotNull(materialStatisticsAsync);
        Assert.IsFalse(!materialStatisticsAsync.Any());
    }

    /// <summary />
    [TestMethod]
    public async Task Solution_ProducedParts()
    {
        var intelliDivide = GetIntelliDivideClient();

        await OptimizationResultSamples.GetSolutionPartsSample(intelliDivide);
    }
}