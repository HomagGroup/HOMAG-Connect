using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Update.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards
{ 
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]

    public class UpdateBoardsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task BoardsRemoveAllBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveAllBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveSingleBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveSingleBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveSubsetBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveSubsetBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsStoreBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_StoreBoardEntity(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsUpdateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(MaterialAssistClient);
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "23"]);
        }

        [ClassInitialize]
        public async Task Initialize()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
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
                BoardCode = "RP_EG_H3303_ST10_19_2800.0_2070.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }
    }
}