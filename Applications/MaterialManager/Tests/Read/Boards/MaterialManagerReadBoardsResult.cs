using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.MaterialManager.Samples.Read.Boards;
using HomagConnect.MaterialManager.Tests.Base;

namespace HomagConnect.MaterialManager.Tests.Read.Boards
{
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Board.Read.Results")]
    public class MaterialManagerReadBoardsResult : MaterialManagerTestBase
    {
        [TestMethod]
        /// </summary>
        public async Task GetMaterialCodes_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();

            await MaterialManagerReadBoardsResults.GetMaterialCodes(materialManager);
        }

        [TestMethod]
        public async Task GetThumbnails_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();

            await MaterialManagerReadBoardsResults.GetThumbnails(materialManager);
        }

        [TestMethod]
        public async Task GetLocations_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();

            await MaterialManagerReadBoardsResults.GetLocations(materialManager, new List<string>{ "P2_Graphitschwarz_19.0_2800_2070", "P2_Lichtgrau_19_2800_2070" });
        }
    }
}
