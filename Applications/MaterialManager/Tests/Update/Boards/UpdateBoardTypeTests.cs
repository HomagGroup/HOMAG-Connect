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
    public class UpdateBoardTypeTests : MaterialManagerTestBase
    {
        /// <summary />
        [TestInitialize]
        public async Task Initialize()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                //The material code is the identifier of the material type
                MaterialCode = "HPL_F274_9_12.0",
                //The board code is the identifier of the board type
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                Length = 4100.0,
                Width = 650.0,
                Thickness = 19.0,
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
            await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient.Material.Boards);
        }
        
        [TestCleanup]
        public async Task Cleanup()
        {
            var materialManagerClient = GetMaterialManagerClient();
            await materialManagerClient.Material.Boards.DeleteBoardType("HPL_F274_9_12.0_4100.0_650.0");
        }
    }
}
