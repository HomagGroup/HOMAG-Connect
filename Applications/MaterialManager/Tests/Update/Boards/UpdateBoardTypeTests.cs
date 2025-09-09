using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Boards;
using HomagConnect.MaterialManager.Samples.Update.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Tests.Update.Boards
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Boards")]
    public class UpdateBoardTypeTests : MaterialManagerTestBase
    {
        /// <summary />
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateBoardTypeTests();
            var materialManagerClient = classInstance.GetMaterialManagerClient();
            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                MaterialCode = "HPL_F274_9_12.0",
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                Length = 4100.0,
                Width = 650.0,
                Thickness = 12.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            var result = await materialManagerClient.Material.Boards.CreateBoardType(boardTypeRequest);
        }

        [TestMethod]
        public async Task BoardsUpdateBoardType()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var boardCode = "HPL_F274_9_12.0_4100.0_650.0"; 
            await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient.Material.Boards, boardCode);
        }
        
        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new UpdateBoardTypeTests();
            var materialManagerClient = classInstance.GetMaterialManagerClient();
            await materialManagerClient.Material.Boards.DeleteBoardType("HPL_F274_9_12.0_4100.0_650.0");
        }
    }
}
