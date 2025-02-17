using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Tests.Statistics
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Statistics.Inventory")]
    public class BoardTypeInventoryStatisticsTests : MaterialManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Statistics_GetInventoryByMaterial_NoException()
        {
            var materialClient = GetMaterialManagerClient();

            var materialCodes = new[] { "P2_White_19", "P2_White_8" };

            var to = DateTime.Now.AddDays(-1);
            var from = to.AddMonths(-3);

            var statistics = await materialClient.Material.Boards.GetBoardTypeInventoryHistoryAsync(materialCodes, BoardTypeType.Board, from, to);

            Assert.IsNotNull(statistics);

            Trace(statistics);
        }

        /// <summary />
        [TestMethod]
        public async Task Statistics_GetInventoryByMaterial_ByDay_NoException()
        {
            var materialClient = GetMaterialManagerClient();

            var materialCodes = new[] { "P2_White_19", "P2_White_8" };

            var statistics = await materialClient.Material.Boards.GetBoardTypeInventoryHistoryAsync(materialCodes, BoardTypeType.Board, 90);

            Assert.IsNotNull(statistics);

            Trace(statistics);
        }

        /// <summary />
        [TestMethod]
        public async Task Statistics_GetInventory_NoException()
        {
            var materialClient = GetMaterialManagerClient();

            var to = DateTime.Now.AddDays(-1);
            var from = to.AddMonths(-3);

            var statistics = await materialClient.Material.Boards.GetPartHistoryAsync(from, to, 100);

            Assert.IsNotNull(statistics);

            Trace(statistics);
        }

        /// <summary />
        [TestMethod]
        public async Task Statistics_GetInventory_ByDays_NoException()
        {
            var materialClient = GetMaterialManagerClient();

            var statistics = await materialClient.Material.Boards.GetPartHistoryAsync(60, 10);

            Assert.IsNotNull(statistics);

            Trace(statistics);
        }
    }
}