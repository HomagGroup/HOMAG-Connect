using FluentAssertions;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Samples.Statistics.Material.Client;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Statistics;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Statistics")]
public class StatisticTests : IntelliDivideTestBase
{
#pragma warning disable S2699 // Tests should include assertions
    /// <summary />
    
    [TestMethod]
    public async Task Statistics_GetMaterial_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialStatistics = await intelliDivide.GetMaterialStatistics(DateTime.Now.AddDays(-91), DateTime.Now.AddDays(-1), 100).ToListAsync();

        materialStatistics.Should().NotBeNull("Material statistics should be found in the given time period.");
  
        materialStatistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_GetMaterial2_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialStatistics = await intelliDivide.GetMaterialStatistics(90, 100).ToListAsync();

        materialStatistics.Should().NotBeNullOrEmpty("Material statistics should be found in the last 90 days");

        materialStatistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_GetEdgeband_FromTo_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var edgebandStatistics = await intelliDivide.GetEdgebandStatistics(DateTime.Now.AddDays(-91), DateTime.Now.AddDays(-1), 100).ToListAsync();

        edgebandStatistics.Should().NotBeNull("Edgeband statistics should be found in the given time period.");

        edgebandStatistics.Trace();
    }

    [TestMethod]
    public async Task Statistics_GetEdgeband_DaysBack_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var edgebandStatistics = await intelliDivide.GetEdgebandStatistics(30, 100).ToListAsync();

        edgebandStatistics.Should().NotBeNullOrEmpty("Edgeband statistics should be found in the given time period.");

        edgebandStatistics.Trace();
    }

    [TestMethod]
    public async Task Statistics_GetPartSizesByMaterial_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialCodes = new[] { "P2_White_19", "P2_White_8" };

        var to = DateTime.Now.AddDays(-1);
        var from = to.AddMonths(-3);

        var statistics = await intelliDivide.GetPartSizesByMaterialStatistics(materialCodes, from, to).ToListAsync();
        if (statistics == null || !statistics.Any())
        {
            Assert.Inconclusive("No statistics could be found for the given material codes.");
        }

        statistics.Trace();
    }

    [TestMethod]
    public async Task Statistics_GetPartSizesByMaterial2_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialCodes = new[] { "P2_White_19", "P2_White_8" };

        var statistics = await intelliDivide.GetPartSizesByMaterialStatistics(materialCodes, 90).ToListAsync();
        if (statistics == null || !statistics.Any())
        {
            Assert.Inconclusive("No statistics could be found for the given material codes");
        }

        statistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_Sample_RetrieveMaterialStatistics()
    {
        await MaterialStatisticsSample.RetrieveMaterialStatistics(SubscriptionId, AuthorizationKey);
    }
#pragma warning restore S2699 // Tests should include assertions
}