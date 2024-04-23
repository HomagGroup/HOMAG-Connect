using HomagConnect.Base.Extensions;
using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Samples.Statistics.Material.Client;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Statistics;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Statistics")]
public class MaterialStatisticTests : IntelliDivideTestBase
{
#pragma warning disable S2699 // Tests should include assertions
    /// <summary />
    [TestMethod]
    public async Task Statistics_GetMaterial_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialStatistics = await intelliDivide.GetMaterialStatisticsAsync(DateTime.Now.AddDays(-91), DateTime.Now.AddDays(-1), 100).ToListAsync();

        Assert.IsNotNull(materialStatistics);
        Assert.IsFalse(!materialStatistics.Any());

        Trace(materialStatistics);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2024, 05, 15)]
    public async Task Statistics_GetPartSizesByMaterial_NoException()
    {
        var intelliDivide = GetIntelliDivideClient();

        var materialCodes = new[] { "P2_White_19", "P2_White_8" };

        var to = DateTime.Now.AddDays(-1);
        var from = to.AddMonths(-3);

        var statistics = await intelliDivide.GetPartSizesByMaterialStatisticsAsync(materialCodes, from, to);

        Trace(statistics);
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_Sample_RetrieveMaterialStatistics()
    {
        await MaterialStatisticsSample.RetrieveMaterialStatistics(SubscriptionId, AuthorizationKey);
    }
#pragma warning restore S2699 // Tests should include assertions
}