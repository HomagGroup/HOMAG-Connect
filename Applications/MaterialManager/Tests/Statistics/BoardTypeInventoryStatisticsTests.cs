using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Tests.Attributes;

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
    }
}