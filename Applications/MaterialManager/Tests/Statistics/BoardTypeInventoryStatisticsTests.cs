using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Statistics;
using Newtonsoft.Json;
using Shouldly;
using System.Globalization;

namespace HomagConnect.MaterialManager.Tests.Statistics;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Statistics.Inventory")]
public class BoardTypeInventoryStatisticsTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task Statistics_GetInventory_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var to = DateTime.Now.AddDays(-1);
        var from = to.AddMonths(-3);

        var statistics = (await materialClient.Material.Boards.GetPartHistoryAsync(from, to, 100).ConfigureAwait(false) ?? Array.Empty<PartHistory>()).ToArray();

        statistics.ShouldNotBeNull(
            $"because part history should be available from {from:yyyy-MM-dd} to {to:yyyy-MM-dd} with max 100 results");

        statistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_GetInventoryByMaterial_ByDay_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var materialCodes = new[] { "P2_White_19", "P2_White_8" };

        var statistics = (await materialClient.Material.Boards.GetBoardTypeInventoryHistoryAsync(materialCodes, BoardTypeType.Board, 90).ConfigureAwait(false)).ToArray();

        statistics.ShouldNotBeNull(
            $"because inventory history for the last 90 days should be available for material codes '{string.Join(", ", materialCodes)}'");

        statistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task Statistics_GetInventoryByMaterial_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var materialCodes = new[] { "P2_White_19", "P2_White_8" };

        var to = DateTime.Now.AddDays(-1);
        var from = to.AddMonths(-3);

        var statistics = (await materialClient.Material.Boards.GetBoardTypeInventoryHistoryAsync(materialCodes, BoardTypeType.Board, from, to).ConfigureAwait(false)).ToArray();

        statistics.ShouldNotBeNull(
            $"because inventory history should be available for material codes '{string.Join(", ", materialCodes)}' from {from:yyyy-MM-dd} to {to:yyyy-MM-dd}");

        statistics.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task StaticticsBoardTypeInventory_TraceLocalized()
    {
        var materialClient = GetMaterialManagerClient();
        var to = DateTime.Now.AddDays(-1);
        var from = to.AddMonths(-3);
        var statistics = (await materialClient.Material.Boards.GetBoardTypeInventoryHistoryAsync(from, to).ConfigureAwait(false)).ToArray();

        statistics.ShouldNotBeNull();

        var culture = CultureInfo.GetCultureInfo("de-DE");

        var serializedObjectLocalized = statistics.SerializeLocalized(culture);

        var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

        dynamic.ShouldNotBeNull();

        TestContext?.AddResultFile(dynamic.TraceToFile(nameof(StaticticsBoardTypeInventory_TraceLocalized)).FullName);
    }


        /// <summary />
        [TestMethod]
        public async Task StaticticsPartHistory_TraceLocalized()
        {
            var materialClient = GetMaterialManagerClient();
            var to = DateTime.Now.AddDays(-1);
            var from = to.AddMonths(-3);
            var statistics = (await materialClient.Material.Boards.GetPartHistoryAsync(from, to, 100).ConfigureAwait(false) ?? Array.Empty<PartHistory>()).ToArray();

            statistics.ShouldNotBeNull();

            var culture = CultureInfo.GetCultureInfo("de-DE");

            var serializedObjectLocalized = statistics.SerializeLocalized(culture);

            var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

            dynamic.ShouldNotBeNull();

            TestContext?.AddResultFile(dynamic.TraceToFile(nameof(StaticticsPartHistory_TraceLocalized)).FullName);
        }

        /// <summary />
        [TestMethod]
        public async Task StaticticsEdgebandInventory_TraceLocalized()
        {
            var materialClient = GetMaterialManagerClient();
            var to = DateTime.Now.AddDays(-1);
            var from = to.AddMonths(-1);
            var statistics = (await materialClient.Material.Edgebands.GetEdgebandTypeInventoryHistoryAsync(from, to).ConfigureAwait(false) ?? Array.Empty<EdgeInventoryHistory>()).ToArray();

            statistics.ShouldNotBeNull();

            var culture = CultureInfo.GetCultureInfo("de-DE");

            var serializedObjectLocalized = statistics.SerializeLocalized(culture);

            var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

            dynamic.ShouldNotBeNull();

            TestContext?.AddResultFile(dynamic.TraceToFile(nameof(StaticticsEdgebandInventory_TraceLocalized)).FullName);
        }
}