using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Samples.Statistics.Material;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Statistics;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Statistics")]
public class MaterialStatisticTests : IntelliDivideTestBase
{
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

    /// <summary />
    [TestMethod]
    public async Task Statistics_Sample_RetrieveMaterialStatistics()
    {
        await MaterialStatisticsSample.RetrieveMaterialStatistics(SubscriptionId, AuthorizationKey);
    }
}