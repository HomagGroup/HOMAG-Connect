using HomagConnect.Base.Tests.Attributes;
using HomagConnect.MaterialManager.Samples.Read.Boards;

namespace HomagConnect.MaterialManager.Tests.Read.Boards
{
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Board.Read.Results")]
    public class MaterialManagerReadBoardsResult : MaterialManagerTestBase
    {
#pragma warning disable S2699 // Tests should include assertions
        [TestMethod]
        [TemporaryDisabledOnServer(2024, 06, 15)]
        public async Task GetLocations_GetResult_NoException()

        {
            var materialManager = GetMaterialManagerClient();

            await MaterialManagerReadBoardsResults.GetLocations(materialManager.Material.Boards, new List<string> { "P2_Graphitschwarz_19.0_2800_2070", "P2_Lichtgrau_19_2800_2070" });
        }

        [TestMethod]

        [TemporaryDisabledOnServer(2024, 6, 1)]
        public async Task GetMaterialCodes_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();

            await MaterialManagerReadBoardsResults.GetMaterialCodes(materialManager.Material.Boards);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 6, 1)]
        public async Task GetThumbnails_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();

            await MaterialManagerReadBoardsResults.GetThumbnails(materialManager.Material.Boards);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}