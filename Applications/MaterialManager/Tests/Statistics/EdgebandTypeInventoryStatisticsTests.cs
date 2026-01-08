using HomagConnect.Base.Extensions;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Statistics;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Statistics.Inventory")]
public class EdgebandTypeInventoryStatisticsTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task Statistics_GetInventory_ByDays_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var statistics = (await materialClient.Material.Edgebands.GetEdgebandTypeInventoryHistoryAsync(60).ConfigureAwait(false)).ToArray();

        statistics.ShouldNotBeNull(
            "because edgeband type inventory history for the last 60 days should be available");

        statistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_GetInventory_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var to = DateTime.Now.AddDays(-1);
        var from = to.AddMonths(-3);

        var statistics = (await materialClient.Material.Edgebands.GetEdgebandTypeInventoryHistoryAsync(from, to).ConfigureAwait(false)).ToArray();

        statistics.ShouldNotBeNull(
            $"because edgeband type inventory history should be available from {from:yyyy-MM-dd} to {to:yyyy-MM-dd}");

        statistics.Trace();
    }
}