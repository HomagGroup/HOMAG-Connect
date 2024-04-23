using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Tests;
using HomagConnect.Base.Tests.Attributes;

namespace HomagConnect.MaterialManager.Tests.Material.Boards
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Boards")]
    public class BoardTypeInventoryStatisticsTests : MaterialManagerTestBase
    {
        [TestMethod]
        [TemporaryDisabledOnServer(2024, 05, 15)]
        public async Task Statistics_GetInventoryByMaterial_NoException()
        {
            var materialClient = GetMaterialManagerClient();

            var materialCodes = new[] { "P2_White_19", "P2_White_8" };

            var to = DateTime.Now.AddDays(-1);
            var from = to.AddMonths(-3);

            var statistics = await materialClient.Material.Boards.GetBoardTypesByMaterialStatisticsAsync(materialCodes, BoardTypeType.Board ,from, to);

            Trace(statistics);
        }
    }
}
