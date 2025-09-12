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
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;

            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "41111",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "41112",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "41113",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }
        /*
         feature not ready yet
         
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
        */

        [TestMethod]
        public async Task BoardsUpdateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(MaterialAssistClient);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new UpdateBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntities(["41111", "41112", "41113"]);
        }
    }
}