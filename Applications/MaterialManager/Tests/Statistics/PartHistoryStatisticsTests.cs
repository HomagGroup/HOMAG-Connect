using HomagConnect.Base.Tests;
using HomagConnect.Base.Tests.Attributes;

namespace HomagConnect.MaterialManager.Tests.Statistics
{
    public class PartHistoryStatisticsTests
    {
        /// <summary />
        [TestClass]
        [TestCategory("MaterialManager")]
        [TestCategory("MaterialManager.Statistics.Usage")]
        public class EdgebandTypeInventoryStatisticsTests : TestBase
        {
            /// <summary />
            [TestMethod]
            [TemporaryDisabledOnServer(2024, 12, 15)]
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
            [TemporaryDisabledOnServer(2024, 12, 15)]
            public async Task Statistics_GetInventory_ByDays_NoException()
            {
                var materialClient = GetMaterialManagerClient();

                var statistics = await materialClient.Material.Boards.GetPartHistoryAsync(60, 0);

                Assert.IsNotNull(statistics);

                Trace(statistics);
            }
        }
    }
}
