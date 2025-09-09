using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialAssist.Tests.Create.Boards;
using HomagConnect.MaterialAssist.Tests.Update.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Delete.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class DeleteBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new DeleteBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;

            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
                Length = 2800.0,
                Width = 1310.0,
                MaterialCategory = BoardMaterialCategory.Undefined,
                MaterialCode = "MDF_H3171_12_11.6",
                Thickness = 11.6,
                Type = BoardTypeType.Board,
            };
            var newBoardType = await MaterialAssistClient.CreateBoardType(boardTypeRequest);

            var boardTypeRequest2 = new MaterialManagerRequestBoardType()
            {
                BoardCode = "EG_H3303_ST10_19_2800.0_2070.0",
                CoatingCategory = CoatingCategory.MelamineThermoset,
                Grain = Grain.Lengthwise,
                Length = 2800.0,
                Width = 2070.0,
                MaterialCategory = BoardMaterialCategory.Chipboard,
                MaterialCode = "EG_H3303_ST10_19",
                Thickness = 19,
                Type = BoardTypeType.Board,
            };
            var newBoardType2 = await MaterialAssistClient.CreateBoardType(boardTypeRequest2);

            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "50",
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "23",
                BoardCode = "EG_H3303_ST10_19_2800.0_2070.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntity(MaterialAssistClient, "42");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, ["50", "23"]);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new DeleteBoardsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
            await MaterialManagerClient.DeleteBoardType("EG_H3303_ST10_19_2800.0_2070.0");
            await MaterialManagerClient.DeleteBoardType("MDF_H3171_12_11.6_2800.0_1310.0");
        }
    }
}
