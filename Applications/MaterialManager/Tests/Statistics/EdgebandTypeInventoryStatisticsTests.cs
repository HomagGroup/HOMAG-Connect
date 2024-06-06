using HomagConnect.Base.Tests.Attributes;

namespace HomagConnect.MaterialManager.Tests.Statistics
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Statistics.Inventory")]
    public class EdgebandTypeInventoryStatisticsTests : MaterialManagerTestBase
    {
        /// <summary />
        [TestMethod]
        [TemporaryDisabledOnServer(2024, 06, 15)]
        public async Task Statistics_GetInventory_NoException()
        {
            var materialClient = GetMaterialManagerClient();

            var to = DateTime.Now.AddDays(-1);
            var from = to.AddMonths(-3);

            var statistics = await materialClient.Material.Edgebands.GetEdgebandTypeInventoryHistoryAsync(from, to);

            Assert.IsNotNull(statistics);

            Trace(statistics);
        }

   }
}